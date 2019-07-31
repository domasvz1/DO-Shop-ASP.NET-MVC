# MVCLocalDatabaseWebsiteProject
Will be renewed from time to time and updated

And electronic shop local website written with:
- .NET visual Studio 
    * Migartions Entity Framework
- 
(More to come)

You might havesome addons or something else regarding the project, so im leaving you with this "Setting up the project" guide:
--
If you pull this from the master and the error is proceeded with the 

  You can fix it by entering this:
  --
  Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r

  In the "Package Manager Console" in the Visual Studio.

  Also you can add "SQL Server Object Explorer" in view. If itsmissin, restrat the project (by opening it in the explorer using Visual    Studio).



This error may also occur
"No context type was found in the assembly"

In the Packahge Manager Console (Or if it changes in the future in something similar) change Defaukt project to "DataAccess"
--
Enable-Migrations (if its installed do the following)

"Add-Migration InitialCreate"        and            "update-database"
--

Some starting tasks:
- Make coding practises better
- Make a script where the admin user would be inserted intoa database so that would not be needed to do manually)


- Ispect more how databasesare created wit Entity Framework/Migrations using DataAccess
