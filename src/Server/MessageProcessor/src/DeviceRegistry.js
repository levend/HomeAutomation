'use strict'

let TemperatureSensorV1 = require('./device/TemperatureSensorV1')

class DeviceRegistry {

  static getDeviceByMessage (message) {
    // TODO
    return new TemperatureSensorV1(message)
  }
}

module.exports = DeviceRegistry

