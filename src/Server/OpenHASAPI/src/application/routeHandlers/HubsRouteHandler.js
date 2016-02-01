'use strict'

var Hub = require('../BusinessLogic/Hub')

class HubsRouteHandler {
  static getHub (request, reply) {
    Hub.getHub(encodeURIComponent(request.params.hubId),
      (hub) => reply(hub),
      (err) => reply.badRequest(err))
  }

  static addHub (request, reply) {
    Hub.addHub(
      {
        'accountId': encodeURIComponent(request.payload.accountId),
        'homeId': encodeURIComponent(request.payload.homeId),
        'encodeKey': encodeURIComponent(request.payload.encodeKey),
        'decodeKey': encodeURIComponent(request.payload.decodeKey)
      },
      (newHub) => reply(newHub),
      (err) => reply.badRequest(err)
    )
  }

  // reads all sensor values for a hub
  static readAllSensorValues (request) {
    return 'All sensors for hub with id ' + encodeURIComponent(request.params.hubId)
  }
}

module.exports = HubsRouteHandler

