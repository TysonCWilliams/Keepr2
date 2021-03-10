FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY ./bin/Release/net5.0/publish/ App/

WORKDIR /App

# EXPOSE 80

EXPOSE 3306

# RUN dotnet dev-certs https

CMD ASPNETCORE_URLS=http://*:$PORT dotnet keepr2.dll


# # Dockerfile
# FROM mcr.microsoft.com/dotnet/sdk:latest AS build
# WORKDIR /app
# # Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore
# # Copy everything else and build
# COPY . .
# RUN dotnet publish -c Release -o out
# # Build runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:latest
# WORKDIR /app
# COPY --from=build-env /app/out .
# # Run the app on container startup
# # Use your project name for the second parameter
# # e.g. MyProject.dll
# ENTRYPOINT [ "dotnet", "keepr2.dll" ]