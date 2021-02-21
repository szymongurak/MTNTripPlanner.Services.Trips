FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# COPY \scr\MTNTripPlanner.Services.Trip.Api*.csproj ./
# RUN dotnet restore

COPY . .
RUN dotnet publish scr/MTNTripPlanner.Services.Trip.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
# ENV ASPNETCORE_URLS http://*:5000
# ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "MTNTripPlanner.Services.Trip.Api.dll"]