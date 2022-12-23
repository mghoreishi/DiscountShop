#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Discounting/Discounting.API/Discounting.API.csproj", "src/Services/Discounting/Discounting.API/"]
COPY ["src/Common/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/Common/IntegrationEventLogEF/"]
COPY ["src/Services/Discounting/Discounting.Domain/Discounting.Domain.csproj", "src/Services/Discounting/Discounting.Domain/"]
COPY ["src/Common/CSharpFunctionalExtensions/CSharpFunctionalExtensions.csproj", "src/Common/CSharpFunctionalExtensions/"]
COPY ["src/Services/Discounting/Discounting.Infrastructure/Discounting.Infrastructure.csproj", "src/Services/Discounting/Discounting.Infrastructure/"]
RUN dotnet restore "src/Services/Discounting/Discounting.API/Discounting.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Discounting/Discounting.API"
RUN dotnet build "Discounting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discounting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discounting.API.dll"]