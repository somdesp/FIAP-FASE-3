#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0

ENV ASPNETCORE_ENVIRONMENT=Docker

COPY ["bin/Release/net8.0", "app/"]

WORKDIR /app
ENTRYPOINT ["dotnet", "FIAP.TECH.WORK.dll"]