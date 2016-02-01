'use strict'

var config = require('config')
var APIServer = require('./APIServer')

var portNumber = config.get('general').listenerPort
let openApi = new APIServer(portNumber)
openApi.startServer()