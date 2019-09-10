# D O Shop-ASP.NET MVC

This was initially my University project changed and updated. (D O initials of my first and last name)
Since there's a lot of things that can be tweaked and changed, I want to update the scripts and the documentation before releasing.
This won't be marketed or selled. This is a free download project with a fair use, UI remade completely learning from Winkel boostrap examples. Might some lithuanian expressions left, since this was my university project.

An electronic shop local website written with following elemts frameworks (all can be found in the project):
- .NET visual Studio 
    * Migartions 
    * Entity Framework
    * Unity Container
    * Boostrap
    * Winkel Boostrap
    * jQuerry
    * Plain javascript
    * Razor
    * Business, DataAcess and Presentation Architecture layers.


Taken from the Microsoft documentation: https://dotnet.microsoft.com/apps/aspnet/web-apps

* Modern, scalable web apps with .NET and C#
Use .NET and C# to create websites based on HTML5, CSS, and JavaScript that are secure, fast, and can scale to millions of users.

-------
*  Dynamically render HTML with Razor
Razor provides a simple, clean, and lightweight way to create dynamic web content using HTML and C#.

With Razor you can use any HTML or C# feature. You get great editor support for both, including IntelliSense which provides auto-completion, real-time type and syntax checking, and more.

-------
* Seamless integration with the data in the project.
The popular Entity Framework (EF) data access library lets you interact with databases using strongly typed objects.

Most popular databases are supported, including SQLite, SQL Server, MySQL, PostgreSQL, DB2 and more, as well as non-relational stores such as MongoDB, Redis, and Azure Cosmos DB.


More documentation coming, (need to read it first and apply in the website)

Also a section here should be added about winkel boostarp framework.



Designing architecture
(upcoming feature)

# Currently working on
- Uploading this to git



Later move on to:
- Admin Panel's Navigation Bar



- Item display in the shop [Postponed]

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
If there are problems with migrations 

[Migrations sould be tested on new PC when starting the project for the first time]


Some starting tasks:
- Make coding practises better used (in the code itself)
- Make a script where (when launching the program for the first time) the "Admin user" would be inserted into the system's database automatically [so that would not be needed to do manually by the programmer]



Main window should use a btter styling.
- When connecting to the system there should be a window displaying theis problem
( You are blocked in our system, let us know how could we help you_)


----
Hierarchy of the locked login
-> first blocked, 
   -> then email, 
      -> then password
      ---(catch all three exceptions somehow or display in that order).

- Ispect more how databasesare created wit Entity Framework/Migrations using DataAccess


[What needs to be fixed]
--

* Responsive and interactive buttons in the admin panel.
* Responsive buttons in the UI
* Change the language from Lithuanian to English
* Adminas/Edit/16 (Photo modificfaction widnow, back button doesnt work or deosnt exist)



[Upcoming features]
--
Upload pictures, display one as icon and then have an album(array of uploaded pictures) and display them in the item page.


[Harder tasks]
--
Connecting the login (through facebook, twitter google, maybe leave reviews) 


[Whats done or added features and fixes]
--
[ Favro was create for the workflow markups]
- Created Favro for login and adding cards there (increasing the workflow)
- Renewing registration menu window visual UI
- Renewing login menu window visual UI
- Fixed a bug where if you are connected, the app crashes.
- Implemented Navigation bar with Winkel bootsrap
- [States] Logging of and Logging on (displaying that in the Navigation Bar)
- Renamed - modules, layers, methods, also at the same time made it more easily understandable/changeable. [Deleted a little part of unnessesary models]

