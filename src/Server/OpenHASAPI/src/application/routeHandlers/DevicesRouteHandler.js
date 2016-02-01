'use strict'

var Home = require('../BusinessLogic/Device')

class DevicesRouteHandler {
  static getDevice (request, reply) {
    Home.getDevice(encodeURIComponent(request.params.hubId), encodeURIComponent(request.params.deviceId),
      (newEntity) => reply(newEntity),
      (err) => reply.badRequest(err))
  }

  static addDevice (request, reply) {
    Home.addDevice(
      {
        // include request parameters
        'hubId': encodeURIComponent(request.params.hubId),

        // include payload parameters
        'deviceId': encodeURIComponent(request.payload.deviceId),
        'accountId': encodeURIComponent(request.payload.accountId),
        'deviceType': encodeURIComponent(request.payload.deviceType),
        'fwVersion': encodeURIComponent(request.payload.fwVersion)
      },
      (newEntity) => reply(newEntity),
      (err) => reply.badRequest(err)
    )
  }
}

module.exports = DevicesRouteHandler
