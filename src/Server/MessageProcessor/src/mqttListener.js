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

    let self = this // is this needed because ES6 is bad, or node.js doesn't properly support this in a function ?
    // subscribe to a few useful events
    this.client.on('connect', function () {
      Log.info('Connected to MQTT server.')

      // subscribe to the wildcard of the status queue
      self.subscribeTopic(self.client, `${self.topicList.status}/#`)
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
    this.client.on('message', function (topic, message) {
      self.invokeMessageCallback(topic, message)
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
    if (topic.startsWith(this.topicList.status) && (topic.length > this.topicList.status.length + 1)) {
      let username = topic.substring(this.topicList.status.length + 1)
      let typedMessage = Message.messageFromString(message.toString())

      // invoke the message call if username & message are both valid
      if (username.length > 0 && typedMessage != null) {
        this.statusMessageCallback(username, typedMessage)
      }
    }
  }

  subscribeTopic (client, oneTopic) {
    client.subscribe(oneTopic)

    Log.info(`Subscribed to: ${oneTopic}`)
  }
}

// export all public members
module.exports = MqttListener
