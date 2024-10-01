FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./Pharmacies/ ./
RUN dotnet restore
RUN dotnet publish -c $BUILD_CONFIGURATION -o publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
EXPOSE 5088
COPY --from=build /src/publish ./
ENTRYPOINT ["dotnet", "Pharmacies.API.dll"]
