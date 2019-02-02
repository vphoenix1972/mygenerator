# <%= projectName %>
To compile a release version run the following command from the root directory of the project
```
deploy\publish\windows\publish.cmd
```
To run the compiled version run the following
```
cd deploy\dist
run.cmd
```

## Docker
Copy the project to PC with docker    
Run
```
dos2unix deploy/docker/*.sh
dos2unix deploy/docker/scripts/*.sh

chmod +x deploy/docker/*.sh

sudo deploy/docker/build.sh
sudo deploy/docker/run.sh
```

