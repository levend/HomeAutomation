var mqttListener = require('./mqttListener')

function startProcessingMessages () {
  mqttListener.startsListeningForMqttMessages()
}

startProcessingMessages()
