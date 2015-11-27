#!/bin/sh

# ************************************************************************************************************
# configure mosquitto
#
echo password_file /etc/mosquitto/accounts > /etc/mosquitto/conf.d/auth.conf
touch /etc/mosquitto/accounts
chown root.root /etc/mosquitto/accounts
chmod 600 /etc/mosquitto/accounts

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

# change authentication to true in the influx config file
cp /etc/opt/influxdb/influxdb.conf /etc/opt/influxdb/influxdb.conf_backup
cat /etc/opt/influxdb/influxdb.conf | sed 's/auth-enabled = false/auth-enabled = true/' > /etc/opt/influxdb/influxdb_auth.conf
rm /etc/opt/influxdb/influxdb.conf
mv /etc/opt/influxdb/influxdb_auth.conf /etc/opt/influxdb/influxdb.conf

# restart services
service mosquitto restart
service influxdb restart
