#!/bin/sh

BASE_FOLDER=/opt/openhas/application/

cd $BASE_FOLDER

# start the application
pm2 stop OpenHASAPI.js
