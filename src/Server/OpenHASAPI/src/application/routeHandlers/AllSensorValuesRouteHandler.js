'use strict'

var SensorValue = require('../BusinessLogic/Database/SensorValue')

class DevicesRouteHandler {
  static getAllSensorValues (request, reply) {
    SensorValue.getAllSensorValues(encodeURIComponent(request.params.hubId),
      (entities) => reply(entities),
      (err) => reply.badRequest(err))
  }

  static saveAllSensorValues (request, reply) {
    SensorValue.saveAllSensorValues(encodeURIComponent(request.params.hubId), request, reply,
      {

      },
      (response) => reply(response),
      (err) => reply.badRequest(err)
    )
  }
}

module.exports = DevicesRouteHandler
