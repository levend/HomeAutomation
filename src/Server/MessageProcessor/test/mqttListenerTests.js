var assert = require('assert')

describe('mqttListener', function() {
  describe('connectAllTopics', function () {
    it('should just work', function () {
      var listener = new mqttListener()

      listener.connectAllTopics()
    })
  })
})