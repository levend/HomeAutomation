#!/bin/sh

BASE_FOLDER=/opt/openhas/application/
cd $BASE_FOLDER

# start the application
forever stop OpenHASAPI.js 2> /dev/null

exit 0

