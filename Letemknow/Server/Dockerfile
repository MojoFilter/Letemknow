#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Letemknow/Server/Letemknow.Server.csproj", "Letemknow/Server/"]
COPY ["Letemknow/Client/Letemknow.Client.csproj", "Letemknow/Client/"]
COPY ["Letemknow/Shared/Letemknow.Shared.csproj", "Letemknow/Shared/"]
RUN dotnet restore "Letemknow/Server/Letemknow.Server.csproj"
COPY . .
WORKDIR "/src/Letemknow/Server"
RUN dotnet build "Letemknow.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Letemknow.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Letemknow.Server.dll"]