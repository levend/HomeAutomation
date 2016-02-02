'use strict'

var DatabaseContext = require('./DatabaseContext')

class Device {
  // returns the device with the specified id
  static getDevice (hubId, deviceId, accountId, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Device
      .findOne({
        where: {
          deviceId: deviceId,
          hubId: hubId,
          accountId: accountId
        }
      })
      .then(
        (entity) => onFulfilled(entity),
        (err) => onRejected(err))
  }

  // adds the device specified in the first parameter
  static addDevice (device, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Device
      .build(device)
      .save()
      .then(
        (newEntity) => onFulfilled(newEntity),
        (err) => onRejected(err))
  }
}

module.exports = Device
