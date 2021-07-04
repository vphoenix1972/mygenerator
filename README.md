# Development environment

.NET 5.0 SDK & Runtime
NodeJS 12 or higher

```
npm install -g @angular/cli
```

To build the template project run
```
cd projects\TemplateSolution
deploy\publish\windows\publish.cmd
```

To run it
```
cd projects\TemplateSolution\dist
run.cmd
```

To build frontend for debug run the following
```
cd projects\TemplateSolution\src\TemplateProject.Web\frontend
ng build --watch
```

To install generator into yeoman run the following in root directory
```
cd yeoman
npm link
```

# Database support
Project supports several database backends.
To choose a database backend, add the project reference to 'Web' project and change dependency injection in Startup.cs

## Connection strings:
```
Mongo:      mongodb://localhost:27017/mygenerator
Postgres:   Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=mygenerator
SQLite:     Data Source=mygenerator.db;
```
