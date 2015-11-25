#!/bin/sh

# intall pm2 and setup it up so it start at startup
npm install pm2 -g
pm2 startup ubuntu
