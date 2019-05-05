#!/bin/bash -e

# Parse command line arguments
while [[ "$1" != "" ]]; do
  case "$1" in
    "-?" | "-h" | "--help" | "")
      echo
      echo "Usage: build.sh [OPTIONS]"
      echo " -? | -h | --help Display this help message."
      echo " -t <tag> Set tag for Docker image."
      echo
      exit 0
      ;;
    "-t")
      shift
      TAG="$1"
      ;;
  esac
  shift
done

if [ -v $TAG ]; then
  TAG="v1"
fi

docker build --pull -f deploy/docker/appImage/Dockerfile -t template-project:$TAG .