#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Shop/Shop.API/Shopping.API.csproj", "src/Services/Shop/Shop.API/"]
COPY ["src/Services/Shop/Shop.Infrastructure/Shopping.Infrastructure.csproj", "src/Services/Shop/Shop.Infrastructure/"]
COPY ["src/Services/Shop/Shop.Domain/Shopping.Domain.csproj", "src/Services/Shop/Shop.Domain/"]
COPY ["src/Common/CSharpFunctionalExtensions/CSharpFunctionalExtensions.csproj", "src/Common/CSharpFunctionalExtensions/"]
RUN dotnet restore "src/Services/Shop/Shop.API/Shopping.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Shop/Shop.API"
RUN dotnet build "Shopping.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.API.dll"]
