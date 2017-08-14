FROM mongo

COPY init.json /init.json
CMD mongoimport --host mongodb --db ToggleDB --collection Toggle --type json --file /init.json --jsonArray