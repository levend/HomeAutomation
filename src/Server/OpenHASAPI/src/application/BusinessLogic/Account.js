'use strict'

var DatabaseContext = require('./DatabaseContext')

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

  // adds the account specified in the first parameter
  static addAccount (account, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Account
      .build(account)
      .save()
      .then(
        (newAccount) => onFulfilled(newAccount),
        (err) => onRejected(err))
  }
}

module.exports = Account
