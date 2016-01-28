#!/bin/sh

#
# make sure we allow node apps to bind to ports < 1024
#
setcap 'cap_net_bind_service=+ep' `which node`

#
# now start he app
#

BASE_FOLDER=/opt/openhas/application/
cd $BASE_FOLDER

# make sure we install all dependencies
npm install

# start the application
pm2 start OpenHASAPI.js
