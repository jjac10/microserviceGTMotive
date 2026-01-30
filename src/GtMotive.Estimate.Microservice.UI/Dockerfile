# Stage 1: Build the Angular application
FROM node:22-alpine AS build
WORKDIR /app

# Copy package.json and package-lock.json first to leverage Docker cache
COPY package*.json ./
RUN npm ci

# Copy the rest of the application source
COPY . .

# Build the application for production
RUN npm run build -- --configuration production

# Stage 2: Serve the application with Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove default Nginx static assets
RUN rm -rf ./*

# Copy the built Angular application from the build stage
# Note: The output path depends on angular.json. Usually dist/<app-name>/browser or just dist/<app-name>
# Based on angular.json, the project name is "GtMotive.Estimate.Microservice.UI"
COPY --from=build /app/dist/GtMotive.Estimate.Microservice.UI/browser .

# Copy custom Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
