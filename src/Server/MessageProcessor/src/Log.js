'use strict'

var log = require('winston')

class Log {
  static info (message) {
    log.info(message)
  }

  static error (message) {
    log.error(message)
  }
}

module.exports = Log
