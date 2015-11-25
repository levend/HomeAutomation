#!/bin/sh

# make sure we can do 'apt-add-repository'
apt-get -y install python-software-properties

# install mosquitto
apt-add-repository -y ppa:mosquitto-dev/mosquitto-ppa
apt-get update
apt-get -y install mosquitto
apt-get -y install mosquitto-clients

# install influxdb
wget http://influxdb.s3.amazonaws.com/influxdb_0.9.4.2_amd64.deb
dpkg -i influxdb_0.9.4.2_amd64.deb

# install node.js
curl -sL https://deb.nodesource.com/setup_4.x | sudo -E bash -

apt-get install -y nodejs
apt-get install -y build-essential

ln -s /usr/bin/nodejs /usr/bin/node
