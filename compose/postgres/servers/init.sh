#!/bin/sh
set -e

cp /host/config_local.py /pgadmin4/config_local.py
cp /host/servers.json /pgadmin4/servers.json

exec /entrypoint.sh
