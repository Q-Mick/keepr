# Use the .NET Core SDK image as the base image for building the backend
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-backend

# Set the working directory inside the container for the backend
WORKDIR /app

# Copy the dotnet project files to the container
COPY ./keepr ./

# Restore the .NET dependencies
RUN dotnet restore

# Build the backend application
RUN dotnet publish -c Release -o out

# Use a separate node image for building the frontend
FROM node:18 AS build-frontend

# Set the working directory inside the container for the frontend
WORKDIR /app/keepr

# Copy the frontend source code
COPY ./keepr.client .

# Install frontend dependencies
RUN npm install

# Build the frontend
RUN npm run build

# Create a new image using a lighter .NET Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory inside the container for the backend
WORKDIR /app

# Copy the published output from the build-backend to the new container
COPY --from=build-backend /app/out .

# Copy the built frontend files from the build-frontend stage to the wwwroot folder of the container
COPY --from=build-frontend /app/keepr/docs ./wwwroot

# Expose the port your application listens on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "keepr.dll"]