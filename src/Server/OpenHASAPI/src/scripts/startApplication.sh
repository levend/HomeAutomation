#!/bin/sh

#
# make sure we allow node apps to bind to ports < 1024
#
setcap 'cap_net_bind_service=+ep' `which nodejs`

#
# now start he app
#

BASE_FOLDER=/opt/openhas/application/
cd $BASE_FOLDER

# make sure we install all dependencies
npm install

# start the application
forever start OpenHASAPI.js -o /var/log/openhas-output.log -l /var/log/openhas-log.log -e /var/log/openhas-error.log
