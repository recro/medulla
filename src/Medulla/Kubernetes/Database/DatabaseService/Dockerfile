FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DatabaseService.csproj", "DatabaseService/"]
RUN dotnet restore "DatabaseService/DatabaseService.csproj"
COPY . DatabaseService/
WORKDIR "/src/DatabaseService"
RUN dotnet build "DatabaseService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DatabaseService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatabaseService.dll"]
