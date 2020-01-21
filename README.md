# Slackers.Services.Bus
Service Bus implementation that uses MassTransit and RabbitMQ
## How to use
1. `mkdir testApp `
2. ` dotnet new webapp `
3. `dotnet add package Slackers.Services.Bus --version 1.0.0 `
4. add a busOptions section in appsettings.  See [Sample Appsettings](https://github.com/kamabery/Slackers.Services.Bus/blob/master/Samples/ProjectManager/appsettings.json)
5. Set Startup with section.

## TODO
1. Look at adding Kafka
