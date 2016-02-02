'use strict'

var DatabaseContext = require('./DatabaseContext')
var bcrypt = require('bcrypt')

var SALT_WORK_FACTOR = 10

class Account {
  // returns the account with the specified id
  static getAccount (accountId, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Account
      .findOne({
        where: {
          accountId: accountId
        }
      })
      .then(
        (account) => onFulfilled(account),
        (err) => onRejected(err))
  }

  // adds a non-activated account; the password will be hashed
  static addAccount (account, password, onFulfilled, onRejected) {
    // hash the password
    bcrypt.genSalt(SALT_WORK_FACTOR, function (err, salt) {
      if (err) {
        onRejected(err)
        return
      }

      bcrypt.hash(password, salt, function (err, hash) {
        if (err) {
          onRejected(err)
          return
        }

        // capture the hashed password
        account.passwordHash = hash

        // every created account will be non-active by default
        account.active = false

        // now save the account
        DatabaseContext.Instance.Models.Account
          .build(account)
          .save()
          .then(
            (newAccount) => onFulfilled(newAccount),
            (err) => onRejected(err))
      })
    })
  }

  static getActiveAccount (username, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Account
      .findOne({
        where: {
          username: username,
          active: true
        }
      })
      .then(
        (account) => onFulfilled(account),
        (err) => onRejected(err))
  }
}

module.exports = Account
