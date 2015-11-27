'use strict'

var influx = require('influx')
var Log = require('./Log')

class ValuePersister {
  constructor (dbOptions) {
    this.client = new influx.InfluxDB(dbOptions)
  }

  persistPoints (username, points, tags) {
    // don't write anything if the input values are not valid
    if (!username || username.length < 1) {
      Log.error('Username is empty; not saving message.')
      return null
    }
    if (points === null || tags === null) {
      Log.error('Either points or tags are empty; not saving message.')
      return
    }

    // make sure the username is always set for the points we would like to write
    tags.username = username

    this.client.writePoint('temperature', points, tags, (err) => {
      if (err) {
        Log.error(err)
      } else {
        //Log.info('Values persisted to db.')
      }
    })
  }
}

module.exports = ValuePersister
