# MVCLocalDatabaseWebsiteProject
Will be renewed from time to time and updated

And electronic shop local website written with:
- .NET visual Studio 
    * Migartions Entity Framework
    * Unity Container
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
- Make coding practises better used (in the code itself)
- Make a script where (when launching the program for the first time) the "Admin user" would be inserted into the system's database automatically [so that would not be needed to do manually by the programmer]


Login Modal has to have 3 lower tier modals:
- Basic info modal 
- Adress information modal
- Credit card information modal


Main window should use a btter styling.
- When connecting to the system there should be a window displaying theis problem
( You are blocked in our system, let us know how could we help you_)


----
Hierarchy -> first blocked, then email, then password (catch all three exceptions somehow or display in that order).


You can create and delete orders.

[Lithuanian]
--
- Padaryti modal sistema, kur login but sudarytas is modalu, kurie butu (visible/not visible)
- Prideti i uzsakymu krepseli turi nuvesti i kita "modal" kuriame butu galima prisijungti


- Ispect more how databasesare created wit Entity Framework/Migrations using DataAccess


[Fix]
--

Responsive buttons in the admin panel.
Responsive buttons in the UI
Change the language from Lithuanian to English
Adminas/Edit/16 (Photo modificfaction widnow, back button doesnt work or deosnt exist)


[Upcoming features]
--
Upload pictures, display one as icon and then have an album(array of uploaded pictures) and display them in the item page.


[Harder tasks]
--
Connecting the login (through facebook, twitter google, maybe leave reviews) 



Starting from:
--
 - Connecting registration and login it with database (In Progress)
 - Connecting registration and login it with database (In Progress)
   
   
[Whats done]
--

- Renewing registration menu window visual
- Renewing login menud window visual
  
