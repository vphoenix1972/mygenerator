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
FROM microsoft/dotnet:2.1-sdk AS backend-build
WORKDIR /app

RUN apt-get update && apt-get install -y dos2unix

# Copy backend source
COPY src/ ./src
COPY <%= csprojName %>.sln ./

RUN dotnet build "<%= csprojName %>.sln"

FROM backend-build as backend-publish
WORKDIR /app/src/<%= csprojName %>.Web

RUN dotnet publish "<%= csprojName %>.Web.csproj" -c Release -o /app/publish
RUN rm -rf /app/publish/wwwroot

##########################
### Build docker image ###
##########################

FROM microsoft/dotnet:2.1-aspnetcore-runtime-bionic

### Install packages

# Shared
RUN apt-get update && \
    apt-get install -y wget apt-transport-https mc nano htop curl dos2unix

# Install PostgreSQL
ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get install -y gnupg tzdata && \
    ln -fs /usr/share/zoneinfo/Etc/UTC /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata
ENV DEBIAN_FRONTEND=newt

RUN wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add - && \
    echo "deb http://apt.postgresql.org/pub/repos/apt/ trusty-pgdg main" | tee /etc/apt/sources.list.d/pgdg.list && \
    apt-get update -y && \
    apt-get install -y postgresql-9.4 postgresql-client

### Configuration

# Make volume directory
RUN mkdir -p /mnt/volume

# Setup PostgreSQL
RUN sed -i "s/\([#]\?\)\(listen_addresses *= *\).*/\2'*'/" /etc/postgresql/9.4/main/postgresql.conf && \
    echo "host all all all md5" >> /etc/postgresql/9.4/main/pg_hba.conf

COPY deploy/docker/scripts/prepare_postgresql.sql /srv/scripts/prepare_postgresql.sql

# Setup scripts
USER root

RUN useradd -ms /bin/bash app && \
    mkdir -p /srv/app && \
    mkdir -p /srv/scripts

COPY deploy/docker/scripts/service_runner.sh /srv/scripts/service_runner.sh
COPY deploy/docker/scripts/app.sh /srv/app/app.sh
COPY deploy/docker/scripts/entrypoint.sh /srv/scripts/entrypoint.sh
COPY deploy/docker/scripts/startup.sh /srv/scripts/startup.sh
COPY deploy/docker/scripts/startup_postgresql.sh /srv/scripts/startup_postgresql.sh
COPY deploy/docker/scripts/service_postgresql_start.sh /srv/scripts/service_postgresql_start.sh
COPY deploy/docker/scripts/startup_app.sh /srv/scripts/startup_app.sh

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

COPY deploy/docker/scripts/appsettings.json /srv/app/appsettings.json.default
COPY --from=backend-publish /app/publish/NLog.config /srv/app/NLog.config.default

ENTRYPOINT ["/srv/scripts/entrypoint.sh"]