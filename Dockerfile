# Backend build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

WORKDIR /app
COPY ./datingapp/ .
RUN dotnet restore
RUN dotnet publish -c Release -o /publish


# Frontend build

FROM node:20.9-alpine AS ng-build

WORKDIR /app
COPY ./datingapp/client/ /app

RUN npm install
RUN npm run build --prod


# Deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /publish
COPY --from=dotnet-build /publish .
COPY --from=ng-build /app/dist/client/browser/ /publish/wwwroot

EXPOSE 8080

ENTRYPOINT ["dotnet", "/publish/API.dll"]
