#!/bin/bash

set -x

INITIAL_SQL_SCRIPT=${INITIAL_SQL_SCRIPT:-/srv/scripts/prepare_postgresql.sql}

### Starting postgre...

chown -R postgres:postgres /var/lib/postgresql
chown -R postgres:postgres /var/lib/postgresql/*
chmod -R 0700 /var/lib/postgresql

tmp=$(su -m postgres -c "/usr/lib/postgresql/9.4/bin/pg_ctl status -D /var/lib/postgresql/9.4/main")

# Previous command does not assign any value to tmp variable than get status code
# If postgres is not initialized it returns 4
# If postgres is not running it returns 3
if [[ $? == 4 ]]; then
  su -m postgres -c "/usr/lib/postgresql/9.4/bin/initdb -D /var/lib/postgresql/9.4/main -E UTF8"
fi

# Starting service
./srv/scripts/service_postgresql_start.sh

tmp=$(su -m postgres -c "echo '\l' | psql | grep app_db")

# Previous command does not assign any value to tmp variable than get status code
# If grep is empty it returns 1 => no app_db, need to create
if [[ $? == 1 ]]; then
  su -m postgres -c "psql --set ON_ERROR_STOP=on -f \"$INITIAL_SQL_SCRIPT\""
fi
