#!/bin/sh

BASE_FOLDER=/opt/ha
APP_FOLDER=$BASE_FOLDER/application

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
rm influxdb_0.9.4.2_amd64.deb

# install node.js
curl -sL https://deb.nodesource.com/setup_4.x | sudo -E bash -

apt-get install -y nodejs
apt-get install -y build-essential

# intall pm2 and setup it up so it start at startup
npm install pm2 -g
env PATH=$PATH:/usr/local/bin NODE_ENV=production pm2 startup ubuntu -u ha --hp /home/ha

# add rights to node to bind to low ports
apt-get -y install libcap2-bin
setcap cap_net_bind_service=+ep /usr/bin/nodejs

# copy config file to a common place
mkdir -p /etc/ha
cp $APP_FOLDER/src/Server/MessageProcessor/config/default_dist.json /etc/ha/default.json
ln -s /etc/ha/default.json $APP_FOLDER/src/Server/MessageProcessor/config/default.json
