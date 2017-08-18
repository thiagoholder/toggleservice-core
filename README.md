## Synopsis

This project started with the main motivation of studies regarding Feature Toggle, but in a decentralized way, without configuration files and without interruptions in the server.
The solution is being implemented with .Net Standart and .Net Core 1.1 technology.
It is an API Rest that through authentication searches for information about a feature available to the application you are requesting.
Using MongoDB as the database.

## Code Example

| Operation Administrator                                                                                   	| Action                                                                                                                   	|
|-----------------------------------------------------------------------------------------------------------	|--------------------------------------------------------------------------------------------------------------------------	|
| [HttpGet] - http://localhost:57425/api/administrator/toggles                                              	| Return all TooglesDocuments from MongoDB                                                                                 	|
| [HttpGet] -http://localhost:57425/api/administrator/toggles/{uniqueServiceKey}                            	| Returns only the features according to the system name entered                                                           	|
| [HttpGet] -http://localhost:57425/api/administrator/{uniqueServiceKey}/{nameFeature}                      	| Returns only one feature through the system name and the reported feature                                                	|
| [HttpPost] -http://localhost:57425/api/administrator/toggles/{uniqueServiceKey}/feature                   	| Post a new feature to system [FromBody] FeatureModel {Name: string, Enabled: bool, Version: int}                         	|
| [HttpPut] -http://localhost:57425/api/administrator/toggles/{uniqueServiceKey}/feature                    	| Update feature to system [FromBody] FeatureModel{Name: string, Enabled: bool, Version: int}                              	|
| [HttpDelete] - http://localhost:57425/api/administrator/toggles/{uniqueServiceKey}/feature/{featureName}" 	| Delete feature to system .                                                                                               	|
| [HttpPost] - http://localhost:57425/api/administrator/toggles                                             	| Post a new ToggleSystem [FromBody] ToggleModel { _id: togglename, features:[{name: string, enabled: boll, version: int}] 	|
| [HttpDelete] - http://localhost:57425/api/administrator/toggles/{togglename}                              	| Delete a toggle system                                                                                                   	|

| Operation Services                                          	| Action                                                                                     	|
|-------------------------------------------------------------	|--------------------------------------------------------------------------------------------	|
| [HttpGet] - http://localhost:57425/api/features/            	| Returns only the features of the authenticated and authorized system                       	|
| [HttpGet] -http://localhost:57425/api/fatures/{featureName} 	| Returns only one feature through the name entered for the logged in and authorized system. 	|

To connect to the API, authentication is required. We are using openiddict to help us with application authentication streams.

## Motivation

Nowadays many examples of Feature Toggle are through physical files that need to reboot the system so that this toggle will work according to the requested change.
In this example, we are decentralizing this responsibility to a REST API that will return information about the feature, its version and availability. This shows that it is no longer necessary to restart the application server, since it will only perform the authentication in the system and receive the requested information.

## Installation
We are using a docker compose file to perform the upload of the MongoDB database.

```
$docker-compose build
$docker-compose up -d
```
Now you need to compile and run WebApi.
```
$dotnet restore
$dotnet run -f netcoreapp1.1 --server.urls="http://localhost:57425"
```
Now let's run the clients (simulating applications that consume the API)
```
cd\ToggleService\ClientToggle
$dotnet restore
$dotnet run

cd\ToggleService\ClientAdministrator
$dotnet restore
$dotnet run
```

## Tests

At the moment it was only possible to do the unit tests.
I'm working on improving all levels of testing and coverage, but I'll release that version for the idea to continue.

The tests can be run using MSTEST (I will briefly modify it to the .Net Core Tests).
The test framework is xUnit.

## Contributors

If you want to contribute with new ideas, corrections, suggestions: Send me a pull request :)

[Openiddic](https://github.com/openiddict "Openiddic Home Page")
[Feature Toggle Video](https://www.infoq.com/br/presentations/feature-toggles-os-2-lados-do-poder "Feature Toggle Video")

## License

A short snippet describing the license (MIT, Apache, etc.)