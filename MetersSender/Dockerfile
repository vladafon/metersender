#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MetersSender/MetersSender.csproj", "MetersSender/"]
COPY ["MetersSender.Api/MetersSender.Api.csproj", "MetersSender.Api/"]
COPY ["MetersSender.Common/MetersSender.Common.csproj", "MetersSender.Common/"]
COPY ["MetersSender.DataAccess/MetersSender.DataAccess.csproj", "MetersSender.DataAccess/"]
COPY ["MetersSender.Neodom/MetersSender.Neodom.csproj", "MetersSender.Neodom/"]
COPY ["MetersSender.Readings/MetersSender.Readings.csproj", "MetersSender.Readings/"]
COPY ["MetersSender.Saures/MetersSender.Saures.csproj", "MetersSender.Saures/"]
RUN dotnet restore "./MetersSender/./MetersSender.csproj"
COPY . .
WORKDIR "/src/MetersSender"
RUN dotnet build "./MetersSender.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MetersSender.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MetersSender.dll"]