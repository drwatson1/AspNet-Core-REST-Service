# How to create new template

After making any changes do:

1. Select Release configuration for a solution
1. Select ReferenceProject in "Solution Explorer"  and click "Project/Export Template..." menu item from the VS main menu
1. In the appeared dialog box select "Project template" option and "ReferenceProject" in the combobox below and click Next
1. Set the value "ASP.Net Core RESTful Service" as a template name and the "Project template to create production-ready RESTful service based on ASP.Net Core. It contains preconfigured DI-container, logging, CORS, some boilerplate code and other features" as a description
1. Clear checkbox "Automatically import the template into Visual Studio" if you don't want immediately import it and click Finish button
1. Extract all files from the created zip-archive to any folder as you want. Typically, the file can be found in "C:\Users\<YOU>\Documents\Visual Studio 2017\My Exported Templates" folder
1. Open a file "MyTemplate.vstemplate"
1. Replace content of the tag `DefaultName` in the section `TemplateData` to "ASPNetCore.Service" (without quotation)
1. Add tag `NumberOfParentCategoriesToRollUp` to the same section with value '1': `<NumberOfParentCategoriesToRollUp>1</NumberOfParentCategoriesToRollUp>`
1. Add all files from the folder to zip-archive with a name "ASP.Net Core RESTful Service.zip". All added files must be in the root of the archive
1. Copy this file to "ProjectTemplates\ReferenceProjectVSIX\ProjectTemplates\CSharp\.NET Core" folder and replace an existing one
1. Go to VS, expand a ReferenceProjectVSIX project and double click on source.extension.vsixmanifest file
1. Increase minor version number on the tab "Metadata" in the top right corner
1. Rebuild the ReferenceProjectVSIX project and get "ASP.Net Core RESTful Service Template.vsix"

That's all. "ASP.Net Core RESTful Service Template.vsix" can be uploaded to VS Marketplace or installed in VS.