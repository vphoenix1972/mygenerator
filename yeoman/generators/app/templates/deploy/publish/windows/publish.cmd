@echo off

set current_dir=%cd%


rem Publish
echo.
echo Publishing...
echo.

rem Cleanup
rd deploy\dist /q /s

rem Build frontend
cd src\<%= csprojName %>.Web\frontend
call npm install

call npm run build

if %ERRORLEVEL% NEQ 0 (
    goto error
)

cd ..\..\..

rem Build backend and copy files
cd src\<%= csprojName %>.Web
dotnet publish -c Release -o ..\..\deploy\dist
cd ..\..

rem Include run.cmd
copy deploy\publish\windows\run.cmd deploy\dist\run.cmd

goto end

:error
cd %current_dir%
exit /B %ERRORLEVEL%%

:end