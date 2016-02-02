'use strict'

var Hapi = require('hapi')
var Log = require('./Log')
var validateAccountPassword = require('./BusinessLogic/Authentication')

class APIServer {
  constructor (portNumber) {
    this.server = new Hapi.Server()
    this.portNumber = portNumber
  }

  startServer () {
    this.server.connection({ port: this.portNumber })

    // first load the auth plugin
    this.server.register(require('hapi-auth-basic'), (err) => {
      if (err) {
        throw err
      }

      this.server.auth.strategy('simple', 'basic', { validateFunc: validateAccountPassword })
    })

    // load the rest of the plugins
    let plugins = [
      // generic plugins
      { register: require('hapi-boom-decorators') },

      // routes
      { register: require('./routes/hubs.js') },
      { register: require('./routes/accounts.js') },
      { register: require('./routes/homes.js') },
      { register: require('./routes/devices.js') },
      { register: require('./routes/allSensorValues.js') }
    ]

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
