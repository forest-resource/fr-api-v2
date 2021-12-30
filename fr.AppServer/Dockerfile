#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["fr.AppServer/fr.AppServer.csproj", "fr.AppServer/"]
COPY ["fr.Service.Model/fr.Service.Model.csproj", "fr.Service.Model/"]
COPY ["fr.Database.Model/fr.Database.Model.csproj", "fr.Database.Model/"]
COPY ["fr.Core/fr.Core.csproj", "fr.Core/"]
COPY ["fr.Database/fr.Database.csproj", "fr.Database/"]
COPY ["fr.Service/fr.Service.csproj", "fr.Service/"]
RUN dotnet restore "fr.AppServer/fr.AppServer.csproj"
COPY . .
WORKDIR "/src/fr.AppServer"
RUN dotnet build "fr.AppServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fr.AppServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fr.AppServer.dll"]