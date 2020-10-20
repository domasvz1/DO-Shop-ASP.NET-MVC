# DO Shop-ASP.NET MVC
------------------------
Public releases, this website project has not reached the alpha version yet. I have decided to publish this and curretnly I am working on 0.3 release (admin panel). This file will be updated once in a while to let people know what is up with this project.

Initially, this was my University project at Vilnius University. Most of the projects done in the University goes by forgotten and dropped out. I have decided
to continue working on this one and improving my knowledge in some specific fields during the development process.
Since there's a lot of things that can be tweaked and changed, I want to update the scripts and the documentation before releasing.
This website project won't be put up to the market or sold. As of right now, you are able to download the project with fair use freely. UI design remade thoroughly learning from Winkel bootstrap examples. There might some Lithuanian expressions left since this was my university project.

In this project, I am using MVC(Model-Based View) architecture design patterns also I am working with .Net Visual Studio IDE.
An E-shop hosted on localhost and written with the following frameworks (all of those frameworks can be found in use in the project itself):
If by any chance you are working with this right now, you are free to learn the following from this:
    * Migrations 
    * Entity Framework
    * Unity Container
    * Bootstrap
    * Winkel Boostrap
    * jquery
    * Plain javascript
    * Razor
    * Ajax

    The project uses Web Layer architecture:
    -  Presentation layer
    -  Business layer
    -  DataAccess layer


Taken from the Microsoft documentation: https://dotnet.microsoft.com/apps/aspnet/web-apps

* Modern, scalable web apps with .NET and C#
Use .NET and C# to create websites based on HTML5, CSS, and JavaScript that are secure, fast, and can scale to millions of users.

-------
*  Dynamically render HTML with Razor
Razor provides a simple, clean, and lightweight way to create dynamic web content using HTML and C#.

With Razor, you can use any HTML or C# feature. You get excellent editor support for both, including IntelliSense which provides auto-completion, real-time type and syntax checking, and more.

-------
* Seamless integration with the data in the project.
The popular Entity Framework (EF) data access library lets you interact with databases using strongly typed objects.

Most popular databases are supported, including SQLite, SQL Server, MySQL, PostgreSQL, DB2 and more, as well as non-relational stores such as MongoDB, Redis, and Azure Cosmos DB.


More documentation coming, (need to read it first and apply in the website)
Also, later in the future, I will add a section about Winkel bootstrap right here for more information.

-----------------------------------------------------------------------------------------------------------------------------------

## How to launch the project 

You might have some addons or something else regarding the project, so I'm leaving you with this "Setting up the project" guide. If you pull the master to your local repository and the following errors occur, you can fix them using these few steps: 

  - In the "Package Manager Console" (in the Visual Studio) update -> Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
  - When you have opened the solution,  set the "Presentation" Layer as a Startup Project in the Visual Studio's Solution Settings.
  - (Optional) Also, you can add "SQL Server Object Explorer" in view. If it's missing, restart the project (by opening it in the explorer using Visual Studio).

This error may also occur
"No context type was found in the assembly"

In the Packahge Manager Console (Or if it changes in the future in something similar) change Default project to "DataAccess"

"Enable-Migrations" in the Package manager (if its installed do the following). Migrations sould be tested on new PC when starting the project for the first time: 
 * If there are problems with migrations "Add-Migration InitialCreate and update-database" 


-----------------------------------------------------------------------------------------------------------------------------------

# Release 0.4 is being created

Plans for release 0.4:
- First and far the most important feature is to fix the fronend of the main shop and make the payment system;
- (Fix the UI in the SHOP) mian
- Drop headline parameter from Item object from the main database;
- (Fix the payment system)
- Another important thing is to make deleting items from database and importing process work flawlessly;
- Clear the processes, have plans for the current release;
- Webpage for webshop processes
- Create UNIT TESTS for the controllers !!!!
- Fix as mcuh exceptions (try catches as possible);
- Add Sprint diagram, try to work in sprint;
- Add CSS on products page;
- Display products categories in Main Products window;
- Add product search in the Main products window;
- Add the cart option and make cart visible;
- Add the ability to add items into the cart
- After creating user successfully show a (*pop up*) that user was created successfully.
- After updating my info in user profile throw a popup that profile updatet sucessfully;
- Fix Cart UI and checkout for the ITEM;
- Change empty shopping cart's UI;

[Bugs fixed]:
- Check if session has started on the same user. (Previously when user has not disconnected from the system, his session would be over but he would still be connected. This is still hapenning but now when you press on nav bar buttons it redirects you to the login page;

So far what has been done:
- Empty webpage created
- Changed the term meaning Shop -> Products
- Products are now being displayed in the store (In the navigation bar find Products section)


-----------------------------------------------------------------------

 # 0.3 Release [done, but Lacks documentation]

Decided to work on another branch and this needs to be in master. Since this branch is not done yet, will merge everything that was corrected and since it was a lot, decided to merge.

The main fixes on this release:
- Started implementing Admin Index Panel with Winkel bootstrap and redoing everything with its styling suggestions;
- Made sure that pictures would be clickable for admin and that it would display admin pictures;
- Made sure that If item has no proper picture, a proper picture would be displayed from content;
- The Layout of ItemInformation was completely redone;
- Implemented picture modification in ItemModification page;
- Redone the layout changes on Modification and Information pages of Items;
- Make so that items would be clickable in the admin's index panel and hoverable too;
- In ModifyItems Change "Save Item" and "Back" buttons implemented functionality and styling;
- Admin Index Page Layout changes;
- In the Admin Panel Page, the sidebar should implement functions with items, user and orders, Making sidebar options clickable;
- In the Admin Panel Page, implemented sidebar functions with items, user and orders;
- Made Importing functionality and sample layout page;
- Refactored"Title" to "Headline" to make more sense, Title doesn't sound good;
- Changed the login panel to admins and make it similar to user login;
- Implemented Navigation Bar's list where the different pages are being displayed logic for the different states;
- Redone admins login and admins top bar ( needs some work though);
- Fixed frontend warnings about nav element in the HTML documents;

Bug fixes:
- [Issue #05] Fixed - If Item has no category, the admin panel doesn't display anything in the category section
- [Issue #07] Admin panel when selecting functionality button nearby, the board spawns nearby first button every time
- [Issue #4] "Save Item" functionality in the admin panel doesn't work
- [Issue #1] Admins Login Text is in the middle of the column, no more
- [Issue #2] When pressed on login column, the column border disappears


-----------------------------------------------------------------------

## Release 0.2 [done]

- Added Country and City classes and implemented the architecture in the Business Object, Data access, Presentation Layers
- Added MobilePhone field in the Business Object, Data access, database also Presentation Layer

- Country and City is now associated via Client Repository
- Fixed issue where selected City or state is null
- A fixed item where if the user has chosen the City and country, the View should know about it and display it
- In "Edit profile" view (save button should lead you back to your edit profile window and *Go Back* button leads to the main page)
** To this fix was added ClientModel.cs which represent Client Data and City and country classes

- Fixed styling-when choosing Cities and Countries in the edit profile window

- Remade EditProfile window, the UI and UX also styling redone

- Fixed implemented --> In EditProfile window changed information now updates with the database.

[Presentation Layer]
- Fixed Selected Country and City Selection lists and styling.
- Fixed the options, when the country is not selected, you cannot select the City aswell.
- Implemented the first option  which won't be hidden, but is always selected as --Not Selected {Delivery Country/City} --

---Not related to coding:

- I did my first Pull Request Review on this project.


-----------------------------------------------------------------------

## [Release 0.1 done]

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

* Web Layer Architecture diagram needs to be done for the first release at least;



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


# Decided to start error log here and how I Dealt with these issues:

Issue: Error thrown The required anti-forgery form field __RequestVerificationToken is not present Error in user Registration
Fix: If you have [ValidateAntiForgeryToken] attribute before your action (In HttpPost, you can't have them in HttpGet),
then you should also add @Html.AntiForgeryToken() in your form after "@using (Html.BeginForm)"
