'use strict'

var Home = require('../BusinessLogic/Database/Home')

class HomesRouteHandler {
  static getHome (request, reply) {
    Home.getHome(encodeURIComponent(request.params.homeId), request.auth.credentials.accountId,
      (home) => reply(home),
      (err) => reply.badRequest(err))
  }

  static addHome (request, reply) {
    Home.addHome(
      {
        'accountId': request.auth.credentials.accountId,
        'name': encodeURIComponent(request.payload.name)
      },
      (newHome) => reply(newHome),
      (err) => reply.badRequest(err)
    )
  }
}

module.exports = HomesRouteHandler
