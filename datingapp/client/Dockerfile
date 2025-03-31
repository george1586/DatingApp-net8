# Build Angular app
FROM node:18 AS build
WORKDIR /app
COPY . .
RUN npm install -g @angular/cli && npm install && ng build --configuration production
 
# Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/dist/client/browser /usr/share/nginx/html
EXPOSE 80
