###############################
### Build project in docker ###
###############################

# Build frontend
FROM node:8.14 as frontend-build
WORKDIR /build

RUN npm install -g @angular/cli

COPY src/<%= csprojName %>.Web/frontend ./src

WORKDIR /build/src

RUN npm install

RUN npm run build

# Build backend
FROM microsoft/dotnet:2.2-sdk AS backend-build
WORKDIR /app

RUN apt-get update && apt-get install -y dos2unix

# Copy backend source
COPY src/ ./src
COPY tests/ ./tests
COPY <%= csprojName %>.sln ./

RUN dotnet build "<%= csprojName %>.sln"

FROM backend-build as backend-publish
WORKDIR /app/src/<%= csprojName %>.Web

RUN dotnet publish "<%= csprojName %>.Web.csproj" -c Release -o /app/publish
RUN rm -rf /app/publish/wwwroot

##########################
### Build docker image ###
##########################

FROM microsoft/dotnet:2.2-aspnetcore-runtime-bionic

### Install packages

# Shared
RUN apt-get update && \
    apt-get install -y wget apt-transport-https mc nano htop curl dos2unix

### Configuration

# Make volume directory
RUN mkdir -p /mnt/volume

# Setup scripts
USER root

RUN useradd -ms /bin/bash app && \
    mkdir -p /srv/app && \
    mkdir -p /srv/scripts

COPY deploy/docker/appImage/service_runner.sh /srv/scripts/service_runner.sh
COPY deploy/docker/appImage/app.sh /srv/app/app.sh
COPY deploy/docker/appImage/entrypoint.sh /srv/scripts/entrypoint.sh
COPY deploy/docker/appImage/startup.sh /srv/scripts/startup.sh

RUN chmod +x /srv/scripts/*.sh && \
    chmod +x /srv/app/*.sh && \
    dos2unix /srv/scripts/*.sh && \
    dos2unix /srv/app/*.sh && \
    ln -s /srv/app/app.sh /etc/init.d/app

RUN chown -R app:app /srv/scripts && \
    chmod -R u=rwX,g=rwX,o=rX /srv/scripts

# Setup project
COPY --from=backend-publish /app/publish /srv/app
COPY --from=frontend-build /build/wwwroot /srv/app/wwwroot

RUN chown -R app:app /srv/app && \
    chmod -R u=rwX,g=rwX,o=rX /srv/app

COPY deploy/docker/appImage/appsettings.json /srv/app/appsettings.json.default
COPY --from=backend-publish /app/publish/NLog.config /srv/app/NLog.config.default

ENTRYPOINT ["/srv/scripts/entrypoint.sh"]