#!/bin/sh

BASE_FOLDER=/opt/ha/application/src/Server/MessageProcessor/src

cd $BASE_FOLDER

pm2 stop MessageProcessorApp.js