services:
  backend:
    build:
      context: ./API
      dockerfile: Dockerfile
    container_name: dotnet_backend
    ports:
      - "5001:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      - frontend
 
  frontend:
    build:
      context: ./client
      dockerfile: Dockerfile
    container_name: angular_frontend
    ports:
      - "4200:80"
