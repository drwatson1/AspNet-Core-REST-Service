# How to create new template

After making any changes do:

1. Select Release configuration for a solution
1. Select ReferenceProject in "Solution Explorer"  and click "Project/Export Template..." menu item from the VS main menu
1. In the appeared dialog box select "Project template" option and "ReferenceProject" in the combobox below and click Next
1. Set the value "ASP.Net WebAPI Application with OWIN" as a template name
1. Clear checkbox "Automatically import the template into Visual Studio" if you don't want immediately import it and click Finish button
1. Extract all files from the created zip-archive to any folder as you want. Typically, the file can be found in "C:\Users\<YOU>\Documents\Visual Studio 2017\My Exported Templates" folder
1. Open ReferenceProject.csproj file in any text editor and find "DocumentationFile" tag
1. Replace string "%24safeprojectname%24" to "$safeprojectname$" inside it and save the file
1. Replace a file "MyTemplate.vstemplate" with the one from the project folder with the same name
1. Add all files from the folder to zip-archive with a name "ASP.Net WebAPI Application with OWIN.zip"
1. Copy this file to "ProjectTemplates\AspNet.WebApi\ReferenceProject\ReferenceProjectVSIX\ProjectTemplates\CSharp\Web" folder and replace an existing one
1. Go to VS, expand a ReferenceProjectVSIX project and double click on source.extension.vsixmanifest file
1. Increase minor version number on the tab "Metadata" in the top right corner
1. Rebuild the ReferenceProjectVSIX project and get "ASP.Net WebAPI Application Project Template.vsix"

That's all. "ASP.Net WebAPI Application Project Template.vsix" can be uploaded to VS Marketplace or installed in VS.