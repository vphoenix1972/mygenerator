#!/bin/bash

sleepPeriod=5
attemptsCount=0
maxAttempts=20

while true; do
    if [ $attemptsCount -ge $maxAttempts ]; then
        echo 'Max attempts ('$maxAttempts') to start PostgreSQL reached!'
        exit 4
    fi

    # pg_isready returns 0 to the shell if the server is accepting connections normally,
    # 1 if the server is rejecting connections (for example during startup),
    # 2 if there was no response to the connection attempt, and 3 if no attempt was made (for example due to invalid parameters).
    #
    # https://www.postgresql.org/docs/9.3/app-pg-isready.html

    pg_isready -q
    pgStatus=$?

    if [ $pgStatus -eq 0 ]; then
        echo 'PostgreSQL up and running.'
        exit 0
    fi

    if [ $pgStatus -eq 1 ]; then
        echo 'PostgreSQL is rejecting connections, waiting...'
        sleep $sleepPeriod
        attemptsCount=$((attemptsCount+1))
        continue
    fi

    if [ $pgStatus -eq 2 ]; then
        echo 'PostgreSQL is not responding, trying to restart...'
        service postgresql start
        sleep $sleepPeriod
        attemptsCount=$((attemptsCount+1))
        continue;
    fi

    echo 'Unknown error, aborting PostgreSQL startup!'

    exit 3

  done