'use strict'

var AccountsRouteHandler = require('../routeHandlers/AccountsRouteHandler.js')

// defines my routes
let myRoutes = [
  {
    method: 'GET',
    path: '/accounts/{accountId}',
    handler: function (request, reply) {
      reply(AccountsRouteHandler.getAccount(request))
    }
  },
  {
    method: 'POST',
    path: '/accounts',
    handler: function (request, reply) {
      reply(AccountsRouteHandler.addAccount(request))
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
