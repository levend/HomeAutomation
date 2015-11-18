'use strict'

let TemperatureSensorV1 = require('./device/TemperatureSensorV1')
let Message = require('./Message')

class DeviceRegistry {

  static getDeviceByMessage (message) {
    // TODO
    return new TemperatureSensorV1(message)
  }
}

module.exports = DeviceRegistry

