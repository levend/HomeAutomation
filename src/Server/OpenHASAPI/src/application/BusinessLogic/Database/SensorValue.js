'use strict'

var DatabaseContext = require('./DatabaseContext')

class SensorValue {
  // returns all sensor values for a hub
  static getAllSensorValues (hubId, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.SensorValue
      .findAll({
        where: {
          hubId: hubId
        }
      })
      .then(
        (entities) => onFulfilled(entities),
        (err) => onRejected(err))
  }

  // adds the home specified in the first parameter
  static saveAllSensorValues (hubId, sensorValues, onFulfilled, onRejected) {
  }
}

module.exports = SensorValue
