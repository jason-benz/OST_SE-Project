#!/bin/bash

PROJECT="MediaHub"
VERSION="1.0.0"
CONTAINER_NAME="mediahubapplication"
CODEFOLDER=/code/code

echo "Script version ${VERSION}"
echo "Starting to update the deplyoment of ${PROJECT} "

checkout_repository () {
    echo "Updating git repository"
    cd ${CODEFOLDER}
    git reset --hard
    git pull
    cp /code/config/appsettings.json ${CODEFOLDER}/MediaHub/MediaHub/appsettings.json
    cp /code/config/appsettings_data.json ${CODEFOLDER}/MediaHub/MediaHub.Data/appsettings.json
    echo "Repository updated"
}

build_container () {
    echo "Starting to build the docker container ${CONTAINER_NAME} locally"
    docker image prune -f
    cd ${CODEFOLDER}/MediaHub
    docker build -f ./MediaHub/Dockerfile -t ${CONTAINER_NAME} .
}

update_running_container () {
    echo "Starting to update current deployment, downtime will occur"
    export PATH="$PATH:$HOME/.dotnet/tools/"
    docker-compose down
    cd ${CODEFOLDER}
    docker-compose up -d
    echo "Updating database"
    cd ${CODEFOLDER}/MediaHub/MediaHub
    sleep 10s
    dotnet-ef database update --connection 'Server=localhost;Database=mediahub;User=SA;Password=SA1234567!s;'
    cd ${CODEFOLDER}/MediaHub/MediaHub.Data
    dotnet-ef database update --connection 'Server=localhost;Database=mediahub_data;User=SA;Password=SA1234567!s;'
}
checkout_repository
build_container
update_running_container
