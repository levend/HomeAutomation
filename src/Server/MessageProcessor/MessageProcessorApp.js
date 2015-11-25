'use strict'

let MessageProcessor = require('./src/MessageProcessor.js')

// create the message processor and start processing messages
let messageProcessor = new MessageProcessor()
messageProcessor.startProcessingMessages()
