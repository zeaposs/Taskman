# About
This is an example application for task management, using SQLite, MassTransit with RabbitMQ and ASP.NET Core Web API.


## Taskman.API
Web API application. Exposes endpoints for creating tasks, updating status of the task and listing all existing tasks.

## Taskman.ConsumerCLI
Message consumer and data manipulation application. Consumes messages provided by the API and changes the state of the database based on the received messages.  

If you're running this application from command line and want to change `appsettings.json` profile, remember to change the `DOTNET_ENVIRONMENT` environment variable to a corresponding value. In case of Windows systems:
```
set DOTNET_ENVIRONMENT=Development
```

# Remarks
Make changes to `appsettings.json` and `appsettings.Development.json` based on your RabbitMQ instance location and credentials. 

