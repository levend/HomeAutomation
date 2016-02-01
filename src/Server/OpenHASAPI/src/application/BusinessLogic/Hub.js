'use strict'

var DatabaseContext = require('./DatabaseContext')

class Hub {
  // returns the hub with the specified id
  static getHub (hubId, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Hub
      .findOne({
        where: {
          hubId: hubId
        }
      })
      .then(
        (hub) => onFulfilled(hub),
        (err) => onRejected(err))
  }

  // adds the hub specified in the first parameter
  static addHub (hub, onFulfilled, onRejected) {
    DatabaseContext.Instance.Models.Hub
      .build(hub)
      .save()
      .then(
        (newHub) => onFulfilled(newHub),
        (err) => onRejected(err))
  }
}

module.exports = Hub
