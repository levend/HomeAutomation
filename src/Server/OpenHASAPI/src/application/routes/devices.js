'use strict'

//
// Base URL: /hubs/{hubId}/devices
//

var DevicesRouteHandler = require('../routeHandlers/DevicesRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/hubs/{hubId}/devices/{deviceId}',
    handler: function (request, reply) {
      DevicesRouteHandler.getDevice(request, reply)
    },
    config: { auth: 'simple' }
  },
  {
    method: 'POST',
    path: '/hubs/{hubId}/devices',
    handler: function (request, reply) {
      DevicesRouteHandler.addDevice(request, reply)
    },
    config: { auth: 'simple' }
  }
]

exports.register = function (server, options, next) {
  server.route(myRoutes)

  next()
}

exports.register.attributes = {
  name: 'devices-routes',
  version: '1.0.0'
}
