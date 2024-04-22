# Engineering challenge

### Service
This is a dotnet service that will do all the CRUD methods to a database.

The implementation is made for the database to be slow and very simple.

There are 2 endpoints that are not implemented `POST /api/v1/Content/{id}/genre` and `DELETE /api/v1/Content/{id}/genre`.

## The Challenge
We want you to fork this repository and try solve the following tasks.
(Remember that you cannot change the `SlowDatabase.cs`)
## Task 1

Implement the missing endpoints:
 * `POST /api/v1/Content/{id}/genre`
    * This endpoint is supposed to add new genres. This should not allow repeated genres to be listed.
 * `DELETE /api/v1/Content/{id}/genre`
    * This endpoint should delete genres.


## Task 2

When the service is runnning in production we have no way of knowing what's happening in the service.
Add logging to the service that you think will give us enough information to know what's happening, but not overload the amount of logging when in Production.

## Task 3

You team has the freedom to choose a new database. Make the necessary changes to adapt to this new connection. (You can choose any tecnology, e.g., Mongodb, Redis, Cassandra, CouchDB, MySQL, etc...)

## Task 4

While your new tecnology isn't Production Ready the  SlowDatabase is really slow (and you cannot change the class).
Find a way to speed up the endpoints without modifying the `SlowDatabase.cs`.

## Task 5

The Project does not have any unit testing, add tests that guarantee that everything works as expected.

## Task 6

We want to deprecate the `GET /api/v1/Content` which always returns all the contents.
Create a new endpoint that deprecates this and implements a way to filter the contents by Title and/or Genre.
(The filter condition can be something as simple as substring and a contains).
