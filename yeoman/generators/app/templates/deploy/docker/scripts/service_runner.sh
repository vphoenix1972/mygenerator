#!/bin/bash

pid="/var/run/$name.pid"
log="/var/log/$name.log"

get_pid() {
    cat "$pid"
}

get_group_pid() {
    echo `ps -w -h -o pgid $1`
}

is_running() {
    # Check 1) pid file exests 2) pid file is not empty 3) process with 'pid' is running
    local is_running=$([ -f "$pid" ] && [ -s "$pid" ] && ps -w -h -o cmd `get_pid`)
    [ ! -z "$is_running" ] > /dev/null 2>&1
}

child_pid() {
    local parent=$1
    local child=$(ps --ppid $parent -o pid h | head -1 | tr -d '[:space:]')

    echo $child
}

function start() {
    if is_running; then
        echo "Already started"
    else
        echo "Starting $name"
        cd "$workdir"
        if [ -z "$runas" ]; then
            $command > $log 2>&1 &
            sleep 1
            echo $! > "$pid"
        else
            su -m "$runas" -c "$command" > $log 2>&1 &
            sleep 1
            child_pid $! > $pid
        fi
        if ! is_running; then
            echo "Unable to start, see $log"
            exit 1
        fi
    fi
}

function stop() {
    if is_running; then
        echo -n "Stopping $name..."

        # Kill all process with the same gpid
        kill -9 -$(get_group_pid $(get_pid))

        for i in {1..10}
        do
            if ! is_running; then
                break
            fi

            echo -n "."
            sleep 1
        done
        echo

        if is_running; then
            echo "Not stopped; may still be shutting down or shutdown may have failed"
            exit 1
        else
            echo "Stopped"
        fi

        if [ -f "$pid" ]; then
            rm "$pid"
        fi
    else
        echo "Not running"
    fi
}

function restart() {
    $0 stop
    if is_running; then
        echo "Unable to stop, will not attempt to start"
        exit 1
    fi
    $0 start
}

function status() {
    if is_running; then
        echo "Running"
    else
        echo "Stopped"
        exit 1
    fi
}

function usage() {
    echo "Usage: $0 {start|stop|restart|status}"
    exit 1
}

case "$1" in
    start)
        start
    ;;
    stop)
        stop
    ;;
    restart)
        restart
    ;;
    status)
        status
    ;;
    *)
        usage
    ;;
esac

exit 0
