@echo off

rem Compile
echo.
echo Compilation...
echo.

cd src\TemplateProject.Web
dotnet build
cd ..\..


rem Publish
echo.
echo Publishing...
echo.

rem Cleanup
rd deploy\dist /q /s

rem Publish
cd src\TemplateProject.Web
call npm install

set NODE_ENV=production

call webpack

set NODE_ENV=

dotnet publish -c Release -o ..\..\deploy\dist
cd ..\..

rem Copy run.cmd
copy deploy\publish\windows\run.cmd deploy\dist\run.cmd