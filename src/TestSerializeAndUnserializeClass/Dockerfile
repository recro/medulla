FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestSerializeAndUnserializeClass/TestSerializeAndUnserializeClass.csproj", "TestSerializeAndUnserializeClass/"]
RUN dotnet restore "TestSerializeAndUnserializeClass/TestSerializeAndUnserializeClass.csproj"
COPY . .
WORKDIR "/src/TestSerializeAndUnserializeClass"
RUN dotnet build "TestSerializeAndUnserializeClass.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestSerializeAndUnserializeClass.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestSerializeAndUnserializeClass.dll"]
