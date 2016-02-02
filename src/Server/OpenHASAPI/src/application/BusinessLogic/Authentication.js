'use strict'

var bcrypt = require('bcrypt')
var Account = require('./Database/Account.js')

var validateAccountPassword = function (request, username, password, callback) {
  Account.getActiveAccount(username,
    (account) => {
      if (!account) {
        return callback(null, false)
      }

      bcrypt.compare(password, account.passwordHash, (err, isValid) => {
        callback(err, isValid, { accountId: account.accountId, username: username })
      })
    },
    (err) => callback(err, false))
}

module.exports = validateAccountPassword
