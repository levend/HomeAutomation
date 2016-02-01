'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('SensorValue', {
    hubId: {
      type: Sequelize.INTEGER,
      field: 'hub_id',
      primaryKey: true
    },
    deviceId: {
      type: Sequelize.STRING(128),
      field: 'device_id',
      primaryKey: true
    },
    sensorIndex: {
      type: Sequelize.INTEGER,
      field: 'sensor_index',
      primaryKey: true
    },
    value: {
      type: Sequelize.STRING(10),
      allowNull: false,
      field: 'value'
    },
    timestamp: {
      type: Sequelize.DATE,
      allowNull: false,
      field: 'timestamp'
    }
  }, {
    tableName: 'sensor_values',
    timestamps: false
  })
}
