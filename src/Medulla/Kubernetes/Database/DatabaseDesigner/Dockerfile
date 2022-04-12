FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app

COPY ./ ./
RUN dotnet publish -c Release -o out DatabaseDesigner.Wasm/DatabaseDesigner.Wasm.csproj

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as final

WORKDIR /app
COPY --from=build /app/out/ ./


FROM nginx:latest

COPY --from=final /app/wwwroot /usr/share/nginx/html

EXPOSE 80

