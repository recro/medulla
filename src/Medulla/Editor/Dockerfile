FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS src
WORKDIR /src
COPY . .
WORKDIR "/src/Server"
EXPOSE 5000

FROM src AS build
WORKDIR "/src/Server"
RUN dotnet restore
RUN dotnet build "Medulla.Frontend.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medulla.Frontend.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medulla.Frontend.Server.dll"]
