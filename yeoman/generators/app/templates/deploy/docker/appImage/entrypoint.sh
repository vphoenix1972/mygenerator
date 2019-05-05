#!/bin/bash

STARTUP_LOG_DIR=${STARTUP_LOG_DIR:-/var/log}
RUN_BASH_AFTER_STARTUP=${RUN_BASH_AFTER_STARTUP:-true}

# Print bash commands before executing them
set -x

. /srv/scripts/startup.sh &> "$STARTUP_LOG_DIR"/startup.log

set +x

if $RUN_BASH_AFTER_STARTUP
    then
    bash
fi