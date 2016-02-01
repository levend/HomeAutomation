'use strict'

var config = require('config')
var Sequelize = require('sequelize')
var Log = require('../Log')

var accountFunction = require('./model/Account')

class Database {
  constructor () {
    this.dbConfiguration = config.get('db')

    this.sequelize = new Sequelize(this.dbConfiguration.database, this.dbConfiguration.user, this.dbConfiguration.password, {
      host: this.dbConfiguration.host,
      dialect: 'mysql',

      pool: {
        max: this.dbConfiguration.maxPoolSize,
        min: 0,
        idle: 10000
      }
    })

    this.registerModels()
    this.start()
  }

  registerModels () {
    // create a placeholder for the models
    this.Models = {}

    // now register all models
    this.Models.Account = accountFunction(this.sequelize)
  }

  start () {
    Log.info(`Database connection is up and running to mysql://${this.dbConfiguration.user}@${this.dbConfiguration.host}/${this.dbConfiguration.database}`)
  }
}

module.exports = Database
