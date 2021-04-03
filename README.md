# TMS-DotNet03-Sinkevich
# Find Housing Project

The FindHousingProject platform is an online housing booking service that provides users (“Guests”) with the ability to post, offer, search, and book services.
Users who post ads and offer services are referred to as "Owners".

# Getting Started

The application is free-to-use.
For using current API you need to register or login by the [following link](https://findhousing.herokuapp.com/).

You can also find repository with front-end part of this application by the [following link](https://github.com/teachmeskills-dotnet/TMS-DotNet03-Sinkevich).

# Application settings
For the correct deploy, it is necessary to create the appsettings.json in the project WebUI directory according to the template below.

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=5432;Database=myDataBase;Userid=myUsername;Password=myPassword;SslMode=Require;TrustServerCertificate=true;"

  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Url": "[following link](https://findhousing.herokuapp.com/)"
}
```
"ConnectionStrings" section contains necessary information for connection to database. In the current case - string for connection to PostgreSQL.

# Deployment to Heroku
After you install the Heroku CLI, run the following login commands:

```
heroku login
heroku container:login
```

For the application (HEROKU_APP) correct work, a database is required. To add PortgreSQL database on [Heroku](https://dashboard.heroku.com/), use the following command:

```
heroku addons:create heroku-postgresql:hobby-dev --app:HEROKU_APP
```
Or add it manually after creating the container in the Resourses tab.

To start the deployment to Heroku, run the following commands from the project folder:
```
docker build -t HEROKU_APP .
docker tag HEROKU_APP registry.heroku.com/HEROKU_APP/web
heroku container:push web -a HEROKU_APP
heroku container:release web -a HEROKU_APP
```
When deploying from Linux, use sudo for the commands above.

# Build with

* [Microsoft.AspNet.Mvc -Version 5.0](https://docs.microsoft.com/ru-ru/aspnet/core/mvc/overview?view=aspnetcore-5.0)
* [AspNetCore.Identity.EntityFrameworkCore -Version 5.0.4](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/5.0.4?_src=template)
* [EntityFrameworkCore.InMemory -Version 5.0.4](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/5.0.4?_src=template)
* [Npgsql.EntityFrameworkCore.PostgreSQL -Version 5.0.2](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/5.0.2?_src=template)
* [EntityFrameworkCore.SqlServer -Version 5.0.4](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.4?_src=template)
* [EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.4?_src=template)
* [EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/5.0.4?_src=template)
* [Abp.Web.Common](https://www.nuget.org/packages/Abp.Web.Common/6.3.0?_src=template)
* [jQuery](https://www.nuget.org/packages/jQuery/3.6.0?_src=template)
* [jspdf.TypeScript.DefinitelyTyped](https://www.nuget.org/packages/jspdf.TypeScript.DefinitelyTyped/0.0.4?_src=template)

# Author
[Julia Sinkevich](https://github.com/julsinkevich) - Developer