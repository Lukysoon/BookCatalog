# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY . .

RUN dotnet restore "BookCatalog/BookCatalog.csproj" --disable-parallel
RUN dotnet publish "BookCatalog/BookCatalog.csproj" -c release -o /app --no-restore

# Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /app ./

ENTRYPOINT [ "dotnet", "BookCatalog.dll" ]