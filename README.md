# Property listing assessment

This is my first time setting up an ASP.NET MVC project from scratch. Apart from moving the data layer into a separate project, everything is pretty much using the template defaults.

The project is a deployed to Azure App Service and uses an Azure SQL DB. It was published manually.

It uses the default ASP.NET Core Identity in the same DB.

My Windows machine is not quite set up for development so I did this on a Mac. Everything worked really well which I was pleasantly surprised at.

## Questions I would have in real life

I guessed a minimal schema for property details.

Is this for a client with an existing system? How would we get the property data? A periodic ETL process would be simpler than trying to query an external system live.

Does the client system already have some kind of user management we would need to use? If not, then at the very least social sign-in should be used.

## Implementation details

There is no customer entity, interest is just recording against the ID of the Identity user. There would also certainly need to be a customer entity in a bigger system.

This is my first time using `IAsyncEnumerable`. It was fine.

I didn't make any attempt to correctly set the culture. Running locally it seems to be using the invariant culture and on the deployed version en-US.

## Issues

Noting interest is a `GET` request, it should be a `POST`. If it status just a boolean toggle, it should probably be an AJAX request.

## Automated testing

There is a unit test project set up to use an in-memory SQLite DB. This would almost certainly be better using another SQL Server instance since the differences between them will get more significant as the project grows (and mean there wouldn't need to be two sets of migrations).

The test project is mostly symbolic since there is not a lot of functionality to test (although a refactor of the `ToggleInterest` on the controller did break the page redirect briefly and that absolutely could have been caught with a unit test).