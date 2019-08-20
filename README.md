# D O Shop

# MVCLocalDatabaseWebsiteProject (D O initials of my first and last name)
This was initially my University project changed and updated.
Since there's a lot of things that can be tweaked and changed, I want to update the scripts and the documentation before releasing.
This won't be marketed or selled. This is a free download project with a fair use, UI remade completely learning from Winkel boostrap examples.

An electronic shop local website written with:
- .NET visual Studio 
    * Migartions 
    * Entity Framework
    * Unity Container
    * Boostrap
    * Winkel Boostrap
- 
(More to come)

# Currently working on
- [States] Logging of and Logging on (displaying that in the Navigation Bar)
- Admin Panel and the 

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


[Lithuanian]
--
- Padaryti modal sistema, kur login but sudarytas is modalu, kurie butu (visible/not visible)
- Prideti i uzsakymu krepseli turi nuvesti i kita "modal" kuriame butu galima prisijungti


- Ispect more how databasesare created wit Entity Framework/Migrations using DataAccess


[What needs to be fixed]
--

* Responsive and interactive buttons in the admin panel.
* Responsive buttons in the UI
* Change the language from Lithuanian to English
* Adminas/Edit/16 (Photo modificfaction widnow, back button doesnt work or deosnt exist)


[Not burning fixes make Favro for this]

[Upcoming features]
--
Upload pictures, display one as icon and then have an album(array of uploaded pictures) and display them in the item page.


[Harder tasks]
--
Connecting the login (through facebook, twitter google, maybe leave reviews) 


[Whats done or added features and fixes]
--
- Created Favro for login and adding cards there (increasing the workflow)
- Renewing registration menu window visual UI
- Renewing login menu window visual UI
- Fixed a bug where if you are connected, the app crashes.
- Implemented Navigation bar with Winkel bootsrap


