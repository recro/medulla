#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Documentation/Server/Medulla.Documentation.Server.csproj", "Documentation/Server/"]
COPY ["Documentation/Client/Medulla.Documentation.Client.csproj", "Documentation/Client/"]
COPY ["Presentation/Assets/Medulla.Presentation.Assets.csproj", "Presentation/Assets/"]
RUN dotnet restore "Documentation/Server/Medulla.Documentation.Server.csproj"
COPY . .
WORKDIR "/src/Documentation/Server"
RUN dotnet build "Medulla.Documentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medulla.Documentation.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medulla.Documentation.Server.dll"]
