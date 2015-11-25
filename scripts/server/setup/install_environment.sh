#!/bin/sh

# make sure we can do 'apt-add-repository'
sudo apt-get -y install python-software-properties

# install mosquitto
sudo apt-add-repository -y ppa:mosquitto-dev/mosquitto-ppa
sudo apt-get update
sudo apt-get -y install mosquitto
sudo apt-get -y install mosquitto-clients

# install influxdb
wget http://influxdb.s3.amazonaws.com/influxdb_0.9.4.2_amd64.deb
sudo dpkg -i influxdb_0.9.4.2_amd64.deb

# install node.js
curl -sL https://deb.nodesource.com/setup_4.x | sudo -E bash -

sudo apt-get install -y nodejs
sudo apt-get install -y build-essential

ln -s /usr/bin/nodejs /usr/bin/node

# intall pm2 and setup it up so it start at startup
npm install pm2 -g
pm2 startup ubuntu
