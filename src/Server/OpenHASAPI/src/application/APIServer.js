'use strict'

var Hapi = require('hapi')
var Log = require('./Log')

class APIServer {
  constructor (portNumber) {
    this.server = new Hapi.Server()
    this.portNumber = portNumber
  }

  startServer () {
    // load all the plugins
    let plugins = [
      // generic plugins
      { register: require('hapi-boom-decorators') },

      // routes
      { register: require('./routes/hubs.js') },
      { register: require('./routes/accounts.js') }
    ]

    this.server.connection({ port: this.portNumber })

    // register plugins, and start the server if none of them fail
    this.server.register(plugins, (err) => {
      if (err) {
        throw err
      }

      this.server.start(() => {
        if (err) {
          throw err
        }

        Log.info('Server running at: ' + this.server.info.uri)
      })
    })
  }
}

module.exports = APIServer
