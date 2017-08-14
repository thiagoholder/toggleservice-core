@echo off
echo Down All Docker Containers
call docker-compose down --remove-orphans
echo Build Docker File
call docker-compose build --force-rm
echo Up Docker compose
call docker-compose up -d --no-build
echo Docker execution