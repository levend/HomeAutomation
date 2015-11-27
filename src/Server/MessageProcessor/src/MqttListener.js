'use strict'

var Log = require('./Log')
var Message = require('./Message')
var mqtt = require('mqtt')

class MqttListener {

  constructor (uri, mqttServerOptions, topicList) {
    this.uri = uri
    this.mqttServerOptions = mqttServerOptions
    this.topicList = topicList

    this.client = undefined
  }

  listenForStatusMessages (callback) {
    this.statusMessageCallback = callback
  }

  startListeningForMqttMessages () {
    Log.info(`Connecting to MQTT server at ${this.uri} ...`)

    this.client = mqtt.connect(this.uri, this.mqttServerOptions)

    // subscribe to a few useful events
    this.client.on('connect', () => {
      Log.info('Connected to MQTT server.')

      // subscribe to the wildcard of the status queue
      this.subscribeTopic(`${this.topicList.status}/#`)
    })

    this.client.on('error', function (err) {
      Log.error('MQTT server connection could not be established. Reason: ' + err.message)
    })

    this.client.on('offline', function () {
      Log.error('MQTT server is offline.')
    })

    this.client.on('close', function () {
      Log.error('Connection to MQTT server was closed.')
    })

    // process the message received on the subscribed topics
    this.client.on('message', (topic, message) => {
      this.invokeMessageCallback(topic, message)
    })
  }

  // redirects the message to it's proper message handler
  invokeMessageCallback (topic, message) {
    if (topic.startsWith(this.topicList.status)) {
      this.processStatusMessage(topic, message)
    }
  }

  // processes the status message, and invokes the callback to handle the status message
  processStatusMessage (topic, message) {
    if (topic.startsWith(this.topicList.status)) {
      let username = topic.substring(this.topicList.status.length + 1)
      let typedMessage = Message.messageFromString(message.toString())

      if (username.length === 0) {
        Log.error('Username was missing from the topic name.')
        return
      }

      if (typedMessage == null) {
        Log.error('Message could not be decomposed.')
        return
      }

      // invoke the message call since username & message are both valid
      this.statusMessageCallback(username, typedMessage)
    }
  }

  subscribeTopic (oneTopic) {
    this.client.subscribe(oneTopic)

    Log.info(`Subscribed to: ${oneTopic}`)
  }
}

// export all public members
module.exports = MqttListener
