'use strict'

var Account = require('../BusinessLogic/Database/Account')

class AccountsRouteHandler {
  static getAccount (request, reply) {
    Account.getAccount(encodeURIComponent(request.params.accountId),
      (account) => reply(account),
      (err) => reply.badRequest(err))
  }

  static addAccount (request, reply) {
    Account.addAccount(
      {
        'username': encodeURIComponent(request.payload.username),
        'email': encodeURIComponent(request.payload.email)
      },
      encodeURIComponent(request.payload.password),
      (newAccount) => reply(newAccount),
      (err) => reply.badRequest(err)
    )
  }
}

module.exports = AccountsRouteHandler
