'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('Device', {
    deviceId: {
      type: Sequelize.STRING(128),
      field: 'device_id',
      primaryKey: true
    },
    hubId: {
      type: Sequelize.INTEGER,
      field: 'hub_id',
      primaryKey: true
    },
    accountId: {
      type: Sequelize.INTEGER,
      field: 'account_id',
      allowNull: false
    },
    deviceType: {
      type: Sequelize.STRING(128),
      allowNull: false,
      field: 'device_type'
    },
    fwVersion: {
      type: Sequelize.INTEGER,
      allowNull: false,
      field: 'fw_version'
    }
  }, {
    tableName: 'devices',
    timestamps: false
  })
}
