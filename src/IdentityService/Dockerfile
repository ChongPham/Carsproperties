FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY Carsproperties.sln ./
COPY src/IdentityService/IdentityService.csproj ./src/IdentityService/
COPY src/Contracts/Contracts.csproj ./src/Contracts/

RUN dotnet restore ./src/IdentityService/IdentityService.csproj

COPY . .
RUN dotnet publish ./src/IdentityService/IdentityService.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]