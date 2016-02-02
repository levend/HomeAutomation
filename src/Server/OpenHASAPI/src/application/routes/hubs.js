'use strict'

//
// Base URL: /hubs
//

var HubsRouteHandler = require('../routeHandlers/HubsRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/hubs/{hubId}',
    handler: function (request, reply) {
      HubsRouteHandler.getHub(request, reply)
    },
    config: { auth: 'simple' }
  },
  {
    method: 'POST',
    path: '/hubs',
    handler: function (request, reply) {
      HubsRouteHandler.addHub(request, reply)
    },
    config: { auth: 'simple' }
  },
  {
    method: 'GET',
    path: '/hubs/{hubId}/all-sensors',
    handler: function (request, reply) {
      reply(HubsRouteHandler.readAllSensorValues(request))
    },
    config: { auth: 'simple' }
  }
]

exports.register = function (server, options, next) {
  server.route(myRoutes)

  next()
}

exports.register.attributes = {
  name: 'hubs-routes',
  version: '1.0.0'
}
