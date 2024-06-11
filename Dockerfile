# Use the .NET 8.0 SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY DayPlannerAPI.sln ./
COPY DayPlannerAPI/DayPlannerAPI.csproj DayPlannerAPI/

# Restore the dependencies
RUN dotnet restore

# Copy the remaining source code and build the project
COPY . .
WORKDIR /src/DayPlannerAPI
RUN dotnet build

# Expose port 80
EXPOSE 80

# Publish the project
RUN dotnet publish --no-restore -c Release -o /app/publish

# Use the ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DayPlannerAPI.dll"]
