FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Todolist.Api/*.csproj Todolist.Api/
COPY Todolist.Api.Infrastructure.Build/*.csproj Todolist.Api.Infrastructure.Build/
COPY Todolist.Api.Tests.Unit/*.csproj Todolist.Api.Tests.Unit/
RUN dotnet restore

COPY . .
WORKDIR /src/Todolist.Api
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Todolist.Api.dll"]

