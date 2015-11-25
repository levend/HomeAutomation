#!/bin/sh

BASE_FOLDER=/opt/ha/application/src/Server/MessageProcessor

cd $BASE_FOLDER

# make sure every package is installed
npm install

# start the server
pm2 start MessageProcessorApp.js

