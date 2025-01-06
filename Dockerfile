# Use the official .NET SDK 8 image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire project and build the release
COPY . ./
RUN dotnet publish -c Release -o /publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /publish

# Copy the published app from the build environment
COPY --from=build-env /publish .

# Expose the required port
EXPOSE 5151

# Set the entry point for the container
ENTRYPOINT ["dotnet", "BlazorApp1.dll"]

