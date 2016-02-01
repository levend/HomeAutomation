'use strict'

var AccountsRouteHandler = require('../routeHandlers/AccountsRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/accounts/{accountId}',
    handler: function (request, reply) {
      AccountsRouteHandler.getAccount(request, reply)
    }
  },
  {
    method: 'POST',
    path: '/accounts',
    handler: function (request, reply) {
      AccountsRouteHandler.addAccount(request, reply)
    }
  }
]

exports.register = function (server, options, next) {
  server.route(myRoutes)

  next()
}

exports.register.attributes = {
  name: 'account-routes',
  version: '1.0.0'
}
