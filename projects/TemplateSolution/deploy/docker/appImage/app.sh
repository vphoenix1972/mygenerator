#!/bin/bash

# Parse command line arguments

action=$1
shift

name="app"
workdir="/srv/app"
runas="app"

command="dotnet TemplateProject.Web.dll"

. /srv/scripts/service_runner.sh $action