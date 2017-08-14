FROM mongo

COPY init.json /init.json
CMD mongoimport --host mongodb:27018 --db ToggleDBTest --collection Toggle --type json --file /init.json --jsonArray