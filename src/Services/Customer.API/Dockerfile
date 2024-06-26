#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Customer.API/Customer.API.csproj", "Services/Customer.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["src/BuildingBlocks/Contracts/Contracts.csproj", "BuildingBlocks/Contracts/"]
COPY ["src/BuildingBlocks/Infrastructure/Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/Shared.csproj", "BuildingBlocks/Shared/"]
RUN dotnet restore "Services/Customer.API/Customer.API.csproj"
COPY . .
WORKDIR "src/Services/Customer.API"
RUN dotnet build "Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.API.dll"]