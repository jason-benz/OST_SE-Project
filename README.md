# MediaHub - Code Repistory

This repository contains the code for our engineering project. This file describes how to use the development environment.

## Local Development

The application has container support and can be startet completely in a container environment. In order to use the containerized version in Visual Studio, the right solution must be choosen. In Rider, the application profile must be customized in order to start the containers.

The application can either be run as container or locally with only the database in a container.

### Containerized Development

The containerized development is best used under Visual Studio, since it does configure itself automatically. This way of working is not recommended and therefore not further described in the documentation.

### Normal Development 

The normal development mode does requrie the developer to start a dockerized Microsoft SQL Server in order to provide the application the needed backend to save the data. Please make sure your development environment has Docker installed.

To start the SQL Server container the first time the following command can be used. The container does also expose the SQL ports on the local machine. This enables the developer to connect to "localhost:1433" to administrate the SQL Server and it's databases. For this various tools can be used.

**Attention:** under Windows the single quots must be replaced with double quotes!

```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SA1234567!s' -p 1433:1433 -d --name db mcr.microsoft.com/mssql/server:2019-latest
```

The container is persistent, therefore it does not get removed automatically even if your restart your machine. But the container has to be started newly if you reboot the machine. The first command displays alle containers on the machine. The last command is used to start a container.

```
docker ps --all
docker stop [CONTAINER ID]
docker start [CONTAINER ID]
```

## Automatic Deployment
The application is automatically build and deployed to an Ubuntu Server 20.04.4 TLS. The server is only reachable from the faculty network or via the VPN client. The host is reachable via SSH with the corresponding private key. [Host FQDN](sifsv-80057.i.ost.ch), [Host IP](152.96.80.57). The deployment is automatically expose on the IP and the FQDN.

The deployment can only be triggered from the main branch and needs to be triggered manually. This enabled the development team to dynamically deploy a new version of the application to the test server.

![Manual Piepline Trigger](img/pipeline.png)

### Deployment Script on Server

The deployment does trigger an SSH connection to the server where the server does update it's code from the main branch of the repository. Afterwards a new docker image is build and the old image/running container is destroyed. The database is persistent and does not get replaced in the process of a new deployment (The bash script lays under: ./server-deployment.sh).

**Attention:** The deployment does exchange the appsettings.json file for an adjusted version with another connection string to the database.
