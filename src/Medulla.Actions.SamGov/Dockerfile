#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Medulla.Actions.SamGov/Medulla.Actions.SamGov.csproj", "Medulla.Actions.SamGov/"]
RUN dotnet restore "Medulla.Actions.SamGov/Medulla.Actions.SamGov.csproj"
COPY . .
WORKDIR "/src/Medulla.Actions.SamGov"
RUN dotnet build "Medulla.Actions.SamGov.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medulla.Actions.SamGov.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medulla.Actions.SamGov.dll"]
