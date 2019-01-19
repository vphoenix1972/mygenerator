#!/bin/bash -e

# Parse command line arguments
while [ "$1" != "" ]; do
    case "$1" in
      "-?" | "-h" | "--help")
        echo
        echo "Usage: run.sh [OPTIONS]"
        echo "   -? | -h | --help                Display this help message."
        echo "   --data-dir <path>               The directory to mount into docker. Default: '/docker-volume'"
        echo "   --clean-run                     Cleans data directory before run. Default: false"
        echo "   --tag <tag>                     Tag for image to run. Default: v1"
        echo
        exit 0
        ;;
      "--data-dir")
        shift
        dataDir="$1"
        ;;
      "--clean-run")
        cleanRun=true
        ;;
      "--tag")
        shift
        tag="$1"
        ;;
      *)
        entry_point_args+=("$1")
        ;;
    esac
    shift
  done

# Apply defaults for unspecified arguments
if [ -v $dataDir ]
  then
    dataDir="/docker-volume"
fi

if [ -v $cleanRun ]
  then
    cleanRun=false
fi

if [ -v $tag ]
  then
    tag="v1"
fi

imageName="<%= dockerImageName %>"

# Print variables
echo
echo imageName="$imageName"
echo dataDir="$dataDir"
echo cleanRun="$cleanRun"
echo tag="$tag"
echo

if $cleanRun; then
    rm -rf $dataDir
fi

# Run image
docker run \
    --name $imageName \
    -v "$dataDir":"/mnt/volume" \
    -v "$dataDir/postgresql":"/var/lib/postgresql/9.4/main" \
    -p 8888:8888 \
    -t -i $imageName:$tag

docker rm $imageName