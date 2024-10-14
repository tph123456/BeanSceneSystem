# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["BeanSceneSystem.csproj", "./"]
RUN dotnet restore "./BeanSceneSystem.csproj"

# Copy the rest of the source code and publish the app
COPY . .
RUN dotnet publish "BeanSceneSystem.csproj" -c Release -o /app/publish

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  # Make sure there's no comment on the same line.

# Copy the published output from the build image
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BeanSceneSystem.dll"]



