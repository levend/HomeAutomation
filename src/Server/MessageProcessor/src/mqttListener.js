var config = require('config')
var mqtt = require('mqtt')
var log = require('winston')

// export all public members
module.exports = {
  startsListeningForMqttMessages: startsListeningForMqttMessages
}

function startsListeningForMqttMessages () {
  // get the mqtt server connection from the configuration file and connect to it
  var mqttServer = config.get('mqtt.uri')
  var mqttServerOptions = config.get('mqtt.options')

  log.info('Connecting to MQTT server at ' + mqttServer + ' ...')

  var client = mqtt.connect(mqttServer, mqttServerOptions)

  // subscribe to a few useful events
  client.on('connect', function () {
    log.info('Connected to MQTT server.')
    connectAllTopics(client)
  })

  client.on('error', function (err) {
    log.error('MQTT server connection could not be established. Reason: ' + err.message)
  })

  client.on('offline', function () {
    log.error('MQTT server is offline.')
  })

  client.on('close', function () {
    log.error('Connection to MQTT server was closed.')
  })

  // process the message received on the subscribed topics
  client.on('message', function (topic, message) {
    log.info('Message received: ' + message.toString())
  })
}

function connectAllTopics (client) {
  var topicList = config.get('mqtt.topicList')

  topicList.forEach(function (oneTopic) {
    client.subscribe(oneTopic)

    log.info('Subscribed to: ' + oneTopic)
  })
}
