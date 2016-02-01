'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('Home', {
    homeId: {
      type: Sequelize.INTEGER,
      field: 'home_id',
      autoIncrement: true,
      primaryKey: true
    },
    accountId: {
      type: Sequelize.INTEGER,
      field: 'account_id',
      allowNull: false
    },
    name: {
      type: Sequelize.STRING(1024),
      allowNull: false,
      field: 'name'
    }
  }, {
    tableName: 'homes',
    timestamps: false
  })
}
