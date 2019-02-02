# Development environment

VS 2017    
ASP.NET Core 2.2.103 SDK, 2.2.1 Runtime    
NodeJS 10 or higher

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

To install generator into yeoman run the following command in root directory
```
npm link
```
