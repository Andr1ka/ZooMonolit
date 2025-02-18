dotnet ef migrations add InitialCreate -s Zoo.API -p Zoo.Infrastructure

dotnet ef database update --project Zoo.Infrastructure --startup-project Zoo.API
