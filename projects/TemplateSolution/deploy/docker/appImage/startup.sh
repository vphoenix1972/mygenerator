#!/bin/bash

# Ensure that configs are in /mnt/volume directory
mkdir -p /mnt/volume

cp -n /srv/app/NLog.config.default /mnt/volume/NLog.config
cp -n /srv/app/appsettings.json.default /mnt/volume/appsettings.json

ln -f -s /mnt/volume/appsettings.json /srv/app/appsettings.json
ln -f -s /mnt/volume/NLog.config /srv/app/NLog.config

mkdir -p /mnt/volume/logs

if [ ! -L /srv/app/logs ]; then
    ln -s /mnt/volume/logs /srv/app/logs
fi

chown -R app:app /mnt/volume
chown -R app:app /mnt/volume/*
chown -R postgres:postgres /mnt/volume/postgresql
chown -R postgres:postgres /mnt/volume/postgresql/*

# Start service
cmd="service app start"

eval $cmd