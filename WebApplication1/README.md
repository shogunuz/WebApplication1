This is a MVC app, which's backend has been written in c#, while front-end has been coded in Razor pages(html). It has DBContext implemenation, 
so, you might find post\get requests to the DB server (MS SQL).

The MVC's features[columns in Database]:
1) Add cities[Id, Name]
2) Add regions [Id, Name]
3) Add states (countries)[Id, Name, State code, Area, CityId, RegionId, Population]

Basically, you can see a list of states in Index page. In addition, if are able to look for a state by clicking on "Find a state" link and typing name of state you want to find out. If the system doesn't find any state with this name, you will be offered to create new one with the name. Moreover, you can reject this offer, if you don't want to add the state with this name, or, you click on "Create a state" and fill all necesarry EditLines in order to create a record in DB->Countries table.

During recording process, system will first look for City in DB->City table, if it doesn't find, the city with typed name will be created automatically. The same thing with Region of state and the state itself.
