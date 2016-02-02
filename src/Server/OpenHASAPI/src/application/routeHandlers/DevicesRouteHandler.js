'use strict'

var Device = require('../BusinessLogic/Database/Device')

class DevicesRouteHandler {
  static getDevice (request, reply) {
    Device.getDevice(encodeURIComponent(request.params.hubId), encodeURIComponent(request.params.deviceId), request.auth.credentials.accountId,
      (newEntity) => reply(newEntity),
      (err) => reply.badRequest(err))
  }

  static addDevice (request, reply) {
    Device.addDevice(
      {
        // include request parameters
        'hubId': encodeURIComponent(request.params.hubId),

        // include payload parameters
        'deviceId': encodeURIComponent(request.payload.deviceId),
        'accountId': request.auth.credentials.accountId,
        'deviceType': encodeURIComponent(request.payload.deviceType),
        'fwVersion': encodeURIComponent(request.payload.fwVersion)
      },
      (newEntity) => reply(newEntity),
      (err) => reply.badRequest(err)
    )
  }
}

module.exports = DevicesRouteHandler
