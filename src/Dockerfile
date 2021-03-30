#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["FindHousingProject.Web/FindHousingProject.Web.csproj", "FindHousingProject.Web/"]
COPY ["FindHousingProject.Common/FindHousingProject.Common.csproj", "FindHousingProject.Common/"]
COPY ["FindHousingProgect.BLL/FindHousingProject.BLL.csproj", "FindHousingProgect.BLL/"]
COPY ["FindHousingProject.DAL/FindHousingProject.DAL.csproj", "FindHousingProject.DAL/"]
RUN dotnet restore "FindHousingProject.Web/FindHousingProject.Web.csproj"
COPY . .
WORKDIR "/src/FindHousingProject.Web"
RUN dotnet build "FindHousingProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindHousingProject.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TMS-DotNet03-Sinkevich.Web.dll