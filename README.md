# ASP.Net Core RESTful Service Template

The repository contains a ready to use preconfigured project template for MS Visual Studio 2017 and 2019 to create fully functional production-ready cross-platform RESTful services based on ASP.Net Core 3.1/5.0.

> Note 1. ASP.NET Core 3.0 has many breaking changes against version 2.2. If you want to use v2.2 or earlier, please, check out one of the previous releases, starting from [version 1.3](https://github.com/drwatson1/AspNet-Core-REST-Service/releases/tag/v1.3) or earlier.
> 
> Note 2. If you are looking for classic ASP.Net services with WebAPI2 and other stuff like that, please, check out a project [
ASP.Net WebApi2 Application with OWIN](https://github.com/drwatson1/AspNet-WebApi).

This template is intended to provide you an almost all that you need for creating RESTful services. The project doesn't contain any UI stuff, like React or Angular libraries, npm, Razor views, as well as any problem domain specific things like EnityFramework, some "right" or "neat" project structure or even a number of projects, each for the specific purpose (for example, service itself, domain logic, data access, testing and so on) as it does some other templates. There are too many areas where ASP.Net Core services can be used, so it can hardly be offered anything related to domain areas to fit all of these project types.

However, some things must be implemented or configured in almost each of the projects, regardless of the subject area. Among these things are dependency injection, exception handling, logging, and others. There are many libraries and solutions to address each of them, but it is impossible to choose a set that fits absolutely everyone. In this project, I have collected the solutions that I consider the most convenient and use them for years in almost each of my projects. I hope you like them too :). So, the project contains these the most important features, but is not limited to them:

- CORS and preflight requests support
- Autofac as a DI-container
- AutoMapper
- Serilog as a default preconfigured logger
- Unhandled exceptions handling
- Swashbuckle for API documentation
- Ability to use environment variables in configuration options and support for '.env' files to easy switching between different environments (thanks to DotNetEnv)
- ... and some boilerplate code

Let me know, what do you think. Any suggestions and bug reports are very appreciated.

# Getting Started

## Using Visual Studio
1. Install the extension from [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=sergey-tregub.asp-net-core-restful-service-template#overview) or download and install the latest version from [GitHub](https://github.com/drwatson1/AspNet-Core-REST-Service/releases/latest). Also, you can install it from Visual Studio. To do so click on "Tools/Extensions and Updates..." menu item, then select "Online/Visual Studio Marketplace/Templates" on the left pane, search for "ASP.Net Core RESTful Service Template," select it and click "Download" button. Please note! The latest version of the template is targeted to the .Net Core 3.1. If you need a template for 2.x version use one of the previous version of the template.
1. Restart Visual Studio
1. Click on "File/New Project..." menu item
1. Expand "Installed/Visual C#/.NET Core" on the left pane
1. Select "APS.Net Core RESTful Service" and click OK button.
1. Select either "IIS Express" or "ASPNetCore.Sevice1" mode and run a service
1. Open a browser and navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) to see an API documentation
1. Play around with the API. Try to add a new product or update one

## Using command line

1. Install the template:
```
dotnet new -i DrWatson1.ProjectTemplate.RestAPI
```
2. Create a project:
```
dotnet new rest-api -n ASPNetCoreService
```
It creates a new project "ASPNetCoreService" in the corresponding subfolder.
Replace the "ASPNetCoreService" with a desired name.

3. Run the project:
```
cd ASPNetCoreService
dotnet run
```
4. Open a browser and navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) to see an API documentation
5. Play around with the API. Try to add a new product or update one

Visit project [Wiki](https://github.com/drwatson1/AspNet-Core-REST-Service/wiki) pages to learn more about the template.

Have fun and happy hacking!

# Release Notes

|Date | Version | Release Notes |
|-----|---------|---------------|
|2021-05-03|2.6|<p>- Support of .Net 5.0<p>- Add `dotnet new` custom template
|2021-03-07|2.5|<p>- Minor fixes |
|2021-02-27|2.4|<p>- Improve logging |
|2021-02-23|2.3|<p>- Load .env before Serilog initialization to make it possible to use environment variables in Serilog configuration options<p>- Catch and log unhandled exceptions <p>- Update NuGet packages to the latest versions|
|2020-08-14|2.2|<p>- Allow Swagger to work behind a proxy<p>- Improve Application Settings with `Options Pattern` and [Configuration Extensions](https://github.com/drwatson1/configuration-extensions)<p>- Update NuGet packages to the latest versions
|2020-01-22|2.1|<p>- Fix deploying to IIS<p>- Fix publishing the service
|2020-01-22|2.0|<p>- Change target framework to .Net Core 3.1<p>- Add health check service
|2020-01-21|1.3|<p>- Update target framework to .Net Core 2.2
|2019-08-22|1.2|<p>- Project tags are added to make it simple to find this project template in a VS2019 project creation wizard<p>- Update NuGet packages to the latest versions|
|2019-04-03|1.1|- Support VS2019
|2019-01-21|1.0|- Initial version
