'use strict'

var Sequelize = require('sequelize')

module.exports = function (sequelize) {
  return sequelize.define('Account', {
    accountId: {
      type: Sequelize.INTEGER,
      field: 'account_id',
      autoIncrement: true,
      primaryKey: true
    },
    username: {
      type: Sequelize.STRING(128),
      allowNull: false,
      field: 'username'
    },
    passwordHash: {
      type: Sequelize.STRING(128),
      allowNull: false,
      field: 'password_hash'
    },
    email: {
      type: Sequelize.STRING(128),
      allowNull: false,
      field: 'email'
    },
    active: {
      type: Sequelize.BOOLEAN,
      allowNull: false,
      field: 'active'
    }
  }, {
    tableName: 'accounts',
    timestamps: false
  })
}
