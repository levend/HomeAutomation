'use strict'

class HubsRouteHandler {
  static readAllSensorValues(request) {
    return 'All sensors for hub with id ' + encodeURIComponent(request.params.hubId)
  }
}

module.exports = HubsRouteHandler
