'use strict'

var Database = require('../db/Database')

// create a database instance
var dbInstance = new Database()

class DatabaseContext {
  static get Instance () {
    return dbInstance
  }
}

module.exports = DatabaseContext
