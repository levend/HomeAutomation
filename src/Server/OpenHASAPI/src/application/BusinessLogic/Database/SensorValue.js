'use strict'

var DatabaseContext = require('./DatabaseContext')
var Log = require('../../Log')

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
  static saveAllSensorValues (hubId, values, onFulfilled, onRejected) {

    // making sure all entries will have this date
    var currentDate = new Date();

    // this will hold the values to create
    var sensorValuesToCreate = [];

    for (var i = 0; i < values.sensorValues.length; i++) {
      var oneValue = values.sensorValues[i]

      sensorValuesToCreate.push(
        {
          hubId: hubId,
          deviceId: oneValue.deviceId,
          sensorIndex: oneValue.sensorIndex,
          value: oneValue.value,
          timestamp: currentDate
        })
    }

    // now bulk insert the values, make sure to update existing sensors (updateOnDuplicate)
    DatabaseContext.Instance.Models.SensorValue
      .bulkCreate(sensorValuesToCreate, { updateOnDuplicate: true })
      .then(
        () => onFulfilled ({}),
        (err) => onRejected(err)
      )
  }
}

module.exports = SensorValue
