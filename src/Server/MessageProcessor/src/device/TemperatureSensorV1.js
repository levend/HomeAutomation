'use strict'

let PersistableDevice = require('./PersistableDevice')

class TemperatureSensorV1 extends PersistableDevice {

  decomposeMessage () {
    if (this.message.payload.length === 2) {
      this.temperature = parseFloat(this.message.payload[1])
      this.sensor = this.message.payload[0]
    }
  }

  get values () {
    if (!this.temperature) {
      return null
    }

    return { value: this.temperature }
  }

  get tags () {
    if (!this.sensor) {
      return null
    }

    return {
      deviceNetwork: this.message.deviceNetwork,
      deviceId: this.message.deviceId,
      sensor: this.sensor
    }
  }
}

module.exports = TemperatureSensorV1
