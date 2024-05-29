# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies
COPY DayPlannerAPI.sln ./
COPY DayPlannerAPI/DayPlannerAPI.csproj DayPlannerAPI/
COPY DayPlannerAPITests/DayPlannerAPITests.csproj DayPlannerAPITests/
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DayPlannerAPI.dll"]
