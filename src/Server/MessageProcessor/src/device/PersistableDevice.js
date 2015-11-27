'use strict'

class PersistableDevice {
  constructor (message) {
    this.message = message

    this.decomposeMessage()
  }

  decomposeMessage () {
    // this will be implemented by subclasses
  }

  get values () {
    return null
  }

  get tags () {
    return null
  }
}

module.exports = PersistableDevice
