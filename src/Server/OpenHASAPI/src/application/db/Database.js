'use strict'

var config = require('config')
var Sequelize = require('sequelize')

class Database {
  constructor () {
    var dbConfiguration = config.get('db')

    this.sequelize = new Sequelize(dbConfiguration.database, dbConfiguration.user, dbConfiguration.password, {
      host: dbConfiguration.host,
      dialect: 'mysql',

      pool: {
        max: dbConfiguration.maxPoolSize,
        min: 0,
        idle: 10000
      }
    })

    this.registerModels()
  }

  registerModels () {

  }
}

