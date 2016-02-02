'use strict'

var DatabaseContext = require('./DatabaseContext')

class Home {
  // returns the home with the specified id
  static getHome (homeId, accountId, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Home
      .findOne({
        where: {
          homeId: homeId,
          accountId: accountId
        }
      })
      .then(
        (home) => onFulfilled(home),
        (err) => onRejected(err))
  }

  // adds the home specified in the first parameter
  static addHome (home, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Home
      .build(home)
      .save()
      .then(
        (newHome) => onFulfilled(newHome),
        (err) => onRejected(err))
  }
}

module.exports = Home
