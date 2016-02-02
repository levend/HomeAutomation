'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('Hub', {
    hubId: {
      type: Sequelize.INTEGER,
      field: 'hub_id',
      autoIncrement: true,
      primaryKey: true
    },
    accountId: {
      type: Sequelize.INTEGER,
      field: 'account_id',
      allowNull: false
    },
    homeId: {
      type: Sequelize.INTEGER,
      field: 'home_id',
      allowNull: false
    },
    name: {
      type: Sequelize.STRING(128),
      allowNull: false,
      field: 'name'
    },
    encodeKey: {
      type: Sequelize.STRING(1024),
      allowNull: false,
      field: 'encode_key'
    },
    decodeKey: {
      type: Sequelize.STRING(1024),
      allowNull: false,
      field: 'decode_key'
    }
  }, {
    tableName: 'hubs',
    timestamps: false
  })
}
