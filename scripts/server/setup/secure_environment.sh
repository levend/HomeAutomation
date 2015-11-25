#!/bin/sh

# ************************************************************************************************************
# configure mosquitto
#
echo password_file /etc/mosquitto/accounts > /etc/mosquitto/conf.d/auth.conf
touch /etc/mosquitto/accounts
chown root.root /etc/mosquitto/accounts
chmod 600 /etc/mosquitto/accounts

# restart mosquitto
service mosquitto restart

# ************************************************************************************************************
# configure influxdb
#

# Read Password
ESCAPE="'"

echo -n Please create a password for the InfluxDB admin user:
read -s INFLUX_PASSWORD
echo

/opt/influxdb/influx -execute "CREATE USER homeauto WITH PASSWORD "$ESCAPE$INFLUX_PASSWORD$ESCAPE" WITH ALL PRIVILEGES"
/opt/influxdb/influx -execute "CREATE DATABASE home_automation"

# set authentication to true for influx
