#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/OcelotAPIGateWay/OcelotAPIGateWay.csproj", "src/ApiGateways/OcelotAPIGateWay/"]
RUN dotnet restore "src/ApiGateways/OcelotAPIGateWay/OcelotAPIGateWay.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/OcelotAPIGateWay"
RUN dotnet build "OcelotAPIGateWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotAPIGateWay.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotAPIGateWay.dll"]
