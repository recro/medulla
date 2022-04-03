# Build the operator
FROM mcr.microsoft.com/dotnet/sdk:latest as build
WORKDIR /app

COPY ./ ./
RUN dotnet publish -c Release -o out Client/Medulla.Shell.Client.csproj

WORKDIR /app/out/wwwroot/Presentation/
COPY Presentation/Assets/wwwroot/ ./

FROM nginx:latest

COPY --from=build /app/out/wwwroot /usr/share/nginx/html

EXPOSE 80

