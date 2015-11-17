'use strict'

class Message {

  constructor (deviceNetwork, deviceId, payload) {
    this._deviceNetwork = deviceNetwork
    this._deviceId = deviceId
    this._payload = payload
  }

  get deviceId () {
    return this._deviceId
  }

  get payload () {
    return this._payload
  }

  static messageFromString (stringMessage) {
    let components = stringMessage.split(',')

    if (components.length >= 2) {
      return new Message(components[0], components[1], components.slice(2))
    } else {
      return null
    }
  }
}

module.exports = Message
