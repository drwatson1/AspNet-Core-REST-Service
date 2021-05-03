# How to create new template

After making any changes do:

**Create a VSIX project template:**
1. Select Release configuration for a solution
1. Select ReferenceProject in "Solution Explorer"  and click "Project/Export Template..." menu item from the VS main menu
1. In the appeared dialog box select "Project template" option and "ReferenceProject" in the combobox below and click Next
1. Set the value `ASP.Net Core RESTful Service` as a template name and the `Project template to create production-ready RESTful service based on ASP.Net Core v3.1 or 5.0. It contains preconfigured DI-container, logging, CORS, some boilerplate code and other features` as a description
1. Don't foget to replace .Net Core version in the description above to the appropriate one.
1. Clear checkbox "Automatically import the template into Visual Studio" if you don't want immediately import it and click Finish button
1. Extract all files from the created zip-archive to any folder as you want. Typically, the file can be found in `C:\Users\<YOU>\Documents\Visual Studio 2019\My Exported Templates` folder
1. Open a file "MyTemplate.vstemplate"
1. Replace content of the tag `DefaultName` in the section `TemplateData` to `ASPNetCore.Service`
1. Add the following tags to the same section:
```xml
    <NumberOfParentCategoriesToRollUp>1</NumberOfParentCategoriesToRollUp>
    <LanguageTag>C#</LanguageTag>
    <PlatformTag>windows</PlatformTag>
    <PlatformTag>linux</PlatformTag>
    <ProjectTypeTag>web</ProjectTypeTag>
    <ProjectTypeTag>RESTful Service</ProjectTypeTag>
```
1. Add all files from the folder to zip-archive with a name `ASP.Net Core RESTful Service.zip`. All added files must be in the root of the archive
1. Copy this file to `ProjectTemplates\ReferenceProjectVSIX\ProjectTemplates\CSharp\.NET Core` folder and replace an existing one
1. Go to VS, expand a ReferenceProjectVSIX project and double click on source.extension.vsixmanifest file
1. Increase minor version number on the tab "Metadata" in the top right corner
1. Update template description
1. Add Release Notes
1. Rebuild the ReferenceProjectVSIX project and get "ASP.Net Core RESTful Service Template.vsix"

**Create a .Net Core Custom template**
1. Open the `DrWatson1.ProjectTemplate.RestAPI.csproj`
1. Update a package version in the `PackageVersion` tag
1. Update a description in the `Description` tag when needed
1. Update release notes in the `PackageReleaseNotes` tag 
1. Open a command prompt and go to the "ProjectTemplates" folder
1. Execute the command:

```
dotnet pack DrWatson1.ProjectTemplate.RestAPI.csproj -c Release
```

That's all. "ASP.Net Core RESTful Service Template.vsix" can be uploaded to VS Marketplace or installed in VS.