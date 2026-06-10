FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build-env

ARG TARGETARCH
WORKDIR /app
COPY . .
RUN dotnet restore ./ZimApi.WebApplication.csproj --arch $TARGETARCH --source https://api.nuget.org/v3/index.json --source https://nuget.bob.house/v3/index.json
RUN dotnet build ./ZimApi.WebApplication.csproj --arch $TARGETARCH --configuration Release --no-restore
RUN dotnet publish ./ZimApi.WebApplication.csproj --arch $TARGETARCH --configuration Release --no-build --no-restore --output /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
EXPOSE 8080/tcp
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "./ZimApi.WebApplication.dll"]
