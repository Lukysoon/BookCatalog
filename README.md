## How to run this project

#### Add configuration
Add appsettings.json file to the BookCatalog project

Example:

    {
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "BookContext": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=heslo;"
      }
    }

#### Enable seed data
1. Go to file *BookCatalog/Migrations/20240925080148_SeedData.cs* and set variable `seedData` to true.

#### Set up database
Install *EF Core tools* if you don't have. In BookCatalog project run `dotnet ef database update` command. It will build database structure and seed example data.

Than you can run the project.
