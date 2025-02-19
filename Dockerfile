# -----------------------------------------
# Stage 1: Build and publish the .NET app
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy all source code into the container
COPY . . 

# Restore dependencies
RUN dotnet restore "AianBlazorPortfolioNet6.csproj"

# Publish the project in Release mode to the /publish folder
RUN dotnet publish "AianBlazorPortfolioNet6.csproj" -c Release -o /app/publish

# -----------------------------------------
# Stage 2: Final runtime image
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/publish .

# Expose port 80 to the outside world
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "AianBlazorPortfolioNet6.dll"]
