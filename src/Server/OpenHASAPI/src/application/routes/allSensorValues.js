'use strict'

//
// Base URL: /hubs/{hubId}/all_sensor_values
//

var AllSensorValuesRouteHandler = require('../routeHandlers/AllSensorValuesRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/hubs/{hubId}/all_sensor_values',
    handler: function (request, reply) {
      AllSensorValuesRouteHandler.getAllSensorValues(request, reply)
    },
    config: { auth: 'simple' }
  },
  {
    method: 'POST',
    path: '/hubs/{hubId}/all_sensor_values',
    handler: function (request, reply) {
      AllSensorValuesRouteHandler.saveAllSensorValues(request, reply)
    },
    config: { auth: 'simple' }
  }
]

exports.register = function (server, options, next) {
  server.route(myRoutes)

  next()
}

exports.register.attributes = {
  name: 'all-sensor-values-routes',
  version: '1.0.0'
}
