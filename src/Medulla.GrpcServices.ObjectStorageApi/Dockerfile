FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Medulla.GrpcServices.ObjectStorageApi/Medulla.GrpcServices.ObjectStorageApi.csproj", "Medulla.GrpcServices.ObjectStorageApi/"]
RUN dotnet restore "Medulla.GrpcServices.ObjectStorageApi/Medulla.GrpcServices.ObjectStorageApi.csproj"
COPY . .
WORKDIR "/src/Medulla.GrpcServices.ObjectStorageApi"
RUN dotnet build "Medulla.GrpcServices.ObjectStorageApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medulla.GrpcServices.ObjectStorageApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medulla.GrpcServices.ObjectStorageApi.dll"]
