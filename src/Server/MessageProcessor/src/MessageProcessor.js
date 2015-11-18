'use strict'

var MqttListener = require('./MqttListener')
var Log = require('./Log')
var DeviceRegistry = require('./DeviceRegistry')
var ValuePersister = require('./ValuePersister')

var config = require('config')

class MessageProcessor {

  constructor () {
    this.mqttConfiguration = config.get('mqtt')
    this.db = config.get('db')

    this.valuePersister = new ValuePersister(this.db)
  }

  startProcessingMessages () {
    let mqttListener = new MqttListener(this.mqttConfiguration.uri, this.mqttConfiguration.options, this.mqttConfiguration.topicList)

    mqttListener.startListeningForMqttMessages()

    mqttListener.listenForStatusMessages( (username, message) => {

      let oneDevice = DeviceRegistry.getDeviceByMessage(message)

      this.valuePersister.persistPoints(username, oneDevice.values, oneDevice.tags)
    })
  }
}

module.exports = MessageProcessor
