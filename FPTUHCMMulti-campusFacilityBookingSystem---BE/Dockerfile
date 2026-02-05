# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files from backend directory
COPY ["FPTUHCMMulti-campusFacilityBookingSystem---BE/Controller/Controller.csproj", "Controller/"]
COPY ["FPTUHCMMulti-campusFacilityBookingSystem---BE/BLL/BLL.csproj", "BLL/"]
COPY ["FPTUHCMMulti-campusFacilityBookingSystem---BE/DAL/DAL.csproj", "DAL/"]
COPY ["FPTUHCMMulti-campusFacilityBookingSystem---BE/Applications/Applications.csproj", "Applications/"]

# Restore dependencies
RUN dotnet restore "Controller/Controller.csproj"

# Copy everything else from backend directory and build
COPY FPTUHCMMulti-campusFacilityBookingSystem---BE/ .
WORKDIR "/src/Controller"
RUN dotnet build "Controller.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Controller.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Controller.dll"]



