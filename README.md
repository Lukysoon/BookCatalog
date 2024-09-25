## How to run this project

#### Add configuration
Add appsetting.json file to the project

Example:

    {
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "BookContext": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=heslo;"
      }
    }

#### Enable seed data
1. Go to file *Migrations/20240925080148_SeedData.cs* and set variable `seedData` to true.

#### Set up database
After installing *EF Core tools* run `dotnet ef database update` comand. It will build database structure and seed example data.

Than you can run the project
