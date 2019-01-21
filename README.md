# ASP.Net Core RESTful Service Template

The repository contains a ready to use preconfigured project template for MS Visual Studio 2017 to create fully functional production-ready cross-platform RESTful services based on ASP.Net Core 2.1.

> Note! If you are looking for classic ASP.Net services with WebAPI2 and other stuff like that, please, check out a project [
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

Read [Wiki](https://github.com/drwatson1/AspNet-Core-REST-Service/wiki) for more information.

Let me know, what do think. Any suggestions and bug reports are very appreciated.
