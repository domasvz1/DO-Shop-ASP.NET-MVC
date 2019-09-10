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


[Tips when launching the project]

- Item display in the shop [Postponed]

You might havesome addons or something else regarding the project, so im leaving you with this "Setting up the project" guide:
--
If you pull this from the master and the error is proceeded with the 

  You can fix it by entering this:
  --
  Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r

  In the "Package Manager Console" in the Visual Studio.

- Also you can add "SQL Server Object Explorer" in view. If itsmissin, restrat the project (by opening it in the explorer using Visual    Studio).
- When you have opened the soloution, make sure to check if  "Presentation" Layer is set as a Startup Project in the Soloution Settings


This error may also occur
"No context type was found in the assembly"

In the Packahge Manager Console (Or if it changes in the future in something similar) change Defaukt project to "DataAccess"
--
Enable-Migrations (if its installed do the following)

Add-Migration InitialCreate and update-database
--
If there are problems with migrations 

[Migrations sould be tested on new PC when starting the project for the first time]
-----------------------------------------------------------------------------------------------------------------------------------


# Currently working on


Later move on to:
- Admin Panel's Navigation Bar


[New added features, fixes, released versions starting with newest at the top]
--
[Version 0.1a released]

- Documented the changes made to the project in the 0.1 alpha version and moved/cleared cards in the Favro from done to documented.

- Renewed Navigation bar and layout, the following Views were renewed (with starts):
* Register _view is somewhat done and testded for clients;
* Login _view is somewhat done and testded for clients;
* Renewed visuals in the Register and Login _Views;
* Added Layout file, from where every page (_view) takes the Layout, Navigation bar, head nad footer;
* Header is done and seperated into a PartialView, when called in the main Layout;
* Footer is done, connected to all pages, seperatedinto a seperate PartialView file;
* Contact, Blog, About, Contact _views were implemeted with some tweaking needed later;
* Home, Shop, Checkout and Items page Layouts in the view is done but will need some tweaking in the later versions as the shop progresses;
* The full Navigation bar is working, functionality has been tested, will need some updates as the shop progresses.


- Merged projects Frontend _Views  with Winkel Boostrap, only the first stage, will need sometweaking in the future;
- The project has been finally updated/released in this git repository;
- Seperate Favro page was created to mark up the goals and the workflow;
- Renewing registration menu window visual UI in the Client Views, also renewing Client controller;
- Renewing login menu window visual UI;
- Fixed a bug where if you are connected, the app crashes;
- Implemented Navigation bar with Winkel bootsrap;
- Fixed scaling bug in all windows when selecting Responsive resolution simulation through Google Screen Emulator (F12);

- [States] Logging of and Logging on (displaying that in the Navigation Bar) for administrators and clients, the following statements describes the idea of the sattes in the shop:
* In the DO Shop, admins are connecting with a secret password and then with their username. On the other end, users are connecting with their emails. The if statements logic was implemented in the navBar itself.
* The Login.cshtml in the "Shared" module was deleted and the logic from it (Users or the admin connection states, is checked in the _NavigationBar.cshtml module, where the main Navigation bar's logic is implemented.
* The 'cart' _View is now seperate and dependant on the connected Client. Admins can't see it.


The follwoing modules, layers, methods, variables were renamed (also at the same time made more easily understandable/changeable) :
- Business Objects Classes and Intercaces;
- Business Logic (The Layer connecting Business Objects with Data Access Layer) Classes and Intercaces;
- Data Access Repository Classes and Interfaces;
- Fixed some naming for Classes, Views, Models, Controllers in Presentation Layer;
- The following three Controllers were tweaked:
* AdminController.cs
* ClientController.cs
* MainController.cs
- All the Models in the Presentation Layer were renamed, fixed and working;
- All the Views of the Controllers, Tweaked, working, will need some work in the future.

The following modules, scripts / classes were deleted :
- IClientPaymentControl, since it was not used.

- Removed unnecessary/unusable files from the Contents;
- Presentation, DataAccess, Bussiness Layers made more readable/ adaptable;
- Removed unnecessary namesapces and imports in Presentation, DataAccess, Bussiness Layers;
- Made fileds with Interface type readonly in all the controllers.

- Fixed JQuerry import issues;


-----------------------------------------------------------------------------------------------------------------------------------


[What needs to be fixed]
--

* Responsive and interactive buttons in the admin panel.
* Responsive buttons in the UI
* Change the language from Lithuanian to English
* Adminas/Edit/16 (Photo modificfaction widnow, back button doesnt work or deosnt exist)


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



[Upcoming features and ideas someday]
--
Upload pictures, display one as icon and then have an album(array of uploaded pictures) and display them in the item page.


Designing architecture
(upcoming feature)


[Harder tasks]
--
Connecting the login (through facebook, twitter google, maybe leave reviews) 



