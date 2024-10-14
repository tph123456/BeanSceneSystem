# Base image containing the .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["BeanSceneSystem.csproj", "./"]
RUN dotnet restore "./BeanSceneSystem.csproj"

# Copy the rest of the source code and publish the app
COPY . .
RUN dotnet publish "BeanSceneSystem.csproj" -c Release -o /app/publish

# Use the runtime base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the command to run the app
ENTRYPOINT ["dotnet", "BeanSceneSystem.dll"]