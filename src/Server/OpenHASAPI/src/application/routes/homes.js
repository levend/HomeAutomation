'use strict'

//
// Base URL: /homes
//

var HomesRouteHandler = require('../routeHandlers/HomesRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/homes/{homeId}',
    handler: function (request, reply) {
      HomesRouteHandler.getHome(request, reply)
    }
  },
  {
    method: 'POST',
    path: '/homes',
    handler: function (request, reply) {
      HomesRouteHandler.addHome(request, reply)
    }
  }
]

exports.register = function (server, options, next) {
  server.route(myRoutes)

  next()
}

exports.register.attributes = {
  name: 'homes-routes',
  version: '1.0.0'
}
