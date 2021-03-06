# Exercise 8


## Learnings

1. Basics about new project system in ASP.NET Core 1.0
1. Running web app with cross-platform web server Kestrel
1. Basics about Node.js-based web tools in Visual Studio 2015
1. Running ASP.NET Core web apps in a Docker container
1. Packaging ASP.NET Core web apps in Docker images using a `Dockerfile`


## ASP.NET Core 1 Web App in Visual Studio 2015

1. Open [Samples/NetCoreSample/NetCoreSample.sln](/Samples/NetCoreSample/) in Visual Studio 2015.

1. Build the solution and make sure that there are no errors.

1. In Visual Studio, look at `project.json` and make yourself familiar with the code.

1. **Discussion points:**
   * New project structure (`.xproj`)
   * New configuration system (JSON instead of XML, options-pattern, etc.)
   * Dependency injection in ASP.NET Core 1.0
   * Integration of web development tools like NPM, Node.js, Gulp, etc. in Visual Studio
   * Code walk-through for Angular 2.0 code 

1. Use Visual Studio's *Task Runner* to run Gulp task `default`.<br/>
   ![Task Runner](/img/visual-studio-run-gulp.png)


## Run ASP.NET Core 1 with Kestrel

1. Open a developer command prompt and navigate to the directory `Samples/NetCoreSample/src/NetCoreSample`.

1. **Discussion points:**
   * Describe basics of ASP.NET Core 1.0 tool `dotnet`

1. Run `dotnet restore` to restore necessary packages from NuGet.

1. Run `npm install` to restore necessary NPM packages.

1. Run `dotnet run` to start your web app using the cross-platform Kestrel web server.<br/>
   ![Run Kestrel](/img/run-kestrel.png)

1. **Discussion points:**
   * Relation of Kestrel and IIS on Windows

1. Open `http://localhost:5000/index.html` to test your web app.


## Run Web App in a Docker Container

1. **Discussion points:**
   * Short introduction into Docker's basic concepts
   * Speak about differences to virtual machines

1. Add a *Docker on Ubuntu* VM to your resource group.<br/>
   ![Docker on Ubuntu](/img/create-docker-vm.png)
   
1. Use an SSH Client (on Windows e.g. *PuTTY*) and connect to your new VM.

1. Make sure that Docker is up and running using: `docker info`

1. Clone the sample repository for this training: `git clone https://github.com/solidifysv/Practical-DevOps-Workshop.git`

1. Get Microsoft's Docker image for ASP.NET Core: `docker pull microsoft/dotnet` 

1. Start a new interactive Docker container: `docker run -it -p 5000:5000 -v ~/Practical-DevOps-Workshop/Samples/NetCoreSample/src/NetCoreSample:/src microsoft/dotnet /bin/bash`

1. **Discussion points:**
   * Describe concept of volume mappings (`-v`) and port mappings (`-p`)

1. Inside of the Docker container, navigate to the mounted `src` folder: `cd src`

1. Restore NuGet packages: `dotnet restore`

1. Run our sample in the Docker container: `dotnet run`

1. If you want to try calling our Web API using your browser, don't forget to open port 5000 for your Docker VM.<br/>
   ![Open Port](/img/azure-open-vm-ports.png)

1. In your browser, open `http://yourvmname.cloudapp.net:5000/api/books`. 
* Check the full url from the Docker VM in Azure

## Create Docker Image for Web App

1. Exit from Docker container **but stay on Docker VM**.

1. Navigate to `Practical-DevOps-Workshop/Sample/NetCoreSample/src/NetCoreSample`: `cd ~/Practical-DevOps-Workshop/Sample/NetCoreSample/src/NetCoreSample`

1. Build image from `Dockerfile`: `docker build -t myaspnet .`

1. **Discussion points:**
   * Speak about basic concepts of Dockerfiles
   * Code walk-through for `Dockerfile`

1. Run container from image: `docker run -d -p 5000:5000 myaspnet`

1. Use `docker ps` and `docker logs` to make sure your container is up and running.

1. If you want to try calling our Web API using your browser, don't forget to open port 5000 for your Docker VM.<br/>
   ![Open Port](/img/azure-open-vm-ports.png)

1. In your browser, open `http://yourvmname.cloudapp.net:5000/api/books`.
