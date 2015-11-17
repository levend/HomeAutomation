'use strict'

var log = require('winston')

class Log {
  static info (message) {
    log.info(message)
  }
}

module.exports = Log
