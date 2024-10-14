# Use the .NET 8.0 SDK for the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["BeanSceneSystem.csproj", "./"]
RUN dotnet restore "./BeanSceneSystem.csproj"

# Copy the rest of the source code and publish the app
COPY . .
RUN dotnet publish "BeanSceneSystem.csproj" -c Release -o /app/publish

# Use the .NET 8.0 runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  # Expose the same port used in Program.cs

# Copy the published output from the build image
COPY --from=build /app/publish .

# Set the command to run the app
ENTRYPOINT ["dotnet", "BeanSceneSystem.dll"]

