'use strict'

var MqttListener = require('./MqttListener')
var Log = require('./Log')

var config = require('config')

class MessageProcessor {

  startProcessingMessages () {
    let mqttListener = new MqttListener(config.get('mqtt.uri'), config.get('mqtt.options'), config.get('mqtt.topicList'))

    mqttListener.startListeningForMqttMessages()

    mqttListener.listenForStatusMessages(this.statusMessageProcessor)
  }

  statusMessageProcessor (username, message) {
    Log.info(`[Status] {${username}} ${message}`)
  }
}

module.exports = MessageProcessor
