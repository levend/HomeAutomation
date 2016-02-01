'use strict'

var config = require('config')
var Sequelize = require('sequelize')
var Log = require('../Log')

var accountFunction = require('./model/Account')
var hubFunction = require('./model/Hub')
var homeFunction = require('./model/Home')
var deviceFunction = require('./model/Device')
var sensorValuesFunction = require('./model/SensorValue')

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
    this.Models.Hub = hubFunction(this.sequelize)
    this.Models.Home = homeFunction(this.sequelize)
    this.Models.Device = deviceFunction(this.sequelize)
    this.Models.SensorValue = sensorValuesFunction(this.sequelize)
  }

  start () {
    Log.info(`Database connection is up and running to mysql://${this.dbConfiguration.user}@${this.dbConfiguration.host}/${this.dbConfiguration.database}`)
  }
}

module.exports = Database
