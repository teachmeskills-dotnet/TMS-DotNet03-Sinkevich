#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/FindHousingProject.Web/FindHousingProject.Web.csproj", "src/FindHousingProject.Web/"]
COPY ["src/FindHousingProject.Common/FindHousingProject.Common.csproj", "src/FindHousingProject.Common/"]
COPY ["src/FindHousingProgect.BLL/FindHousingProject.BLL.csproj", "src/FindHousingProgect.BLL/"]
COPY ["src/FindHousingProject.DAL/FindHousingProject.DAL.csproj", "src/FindHousingProject.DAL/"]
RUN dotnet restore "src/FindHousingProject.Web/FindHousingProject.Web.csproj"
COPY . .
WORKDIR "/src/src/FindHousingProject.Web"
RUN dotnet build "FindHousingProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindHousingProject.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FindHousingProject.Web.dll