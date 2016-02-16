#!/bin/sh
#Start mosquitto
pwd
/usr/local/sbin/mosquitto -d

#Start mongodb
set -m

mongodb_cmd="mongod"
cmd="$mongodb_cmd --httpinterface --rest --master"
if [ "$AUTH" == "yes" ]; then
    cmd="$cmd --auth"
fi

if [ "$JOURNALING" == "no" ]; then
    cmd="$cmd --nojournal"
fi

if [ "$OPLOG_SIZE" != "" ]; then
    cmd="$cmd --oplogSize $OPLOG_SIZE"
fi

$cmd &

if [ ! -f /data/db/.mongodb_password_set ]; then
    ./scripts/set_mongodb_password.sh
fi

#install dependencies
cd /usr/local/docker/app
npm install -verbose

#Start the nodeJS app
node ./bin/www
