FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Database/DatabaseQueryCS/DatabaseQueryCS.csproj", "DatabaseQueryCS/"]
RUN dotnet restore "Database/DatabaseQueryCS/DatabaseQueryCS.csproj"
COPY . .
WORKDIR "/src/DatabaseQueryCS"
RUN dotnet build "DatabaseQueryCS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DatabaseQueryCS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatabaseQueryCS.dll"]
