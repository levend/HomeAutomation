'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('SensorValue', {
    sensorValueId: {
      type: Sequelize.INTEGER,
      field: 'sensor_value_id',
      autoIncrement: true,
      primaryKey: true
    },
    hubId: {
      type: Sequelize.INTEGER,
      field: 'hub_id',
      allowNull: false
    },
    deviceId: {
      type: Sequelize.STRING(128),
      field: 'device_id',
      allowNull: false
    },
    sensorIndex: {
      type: Sequelize.INTEGER,
      field: 'sensor_index',
      allowNull: false
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
