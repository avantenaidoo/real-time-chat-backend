  FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
  WORKDIR /app
  COPY . .
  RUN dotnet restore
  RUN dotnet publish -c Release -o out

  # Runtime stage
  FROM mcr.microsoft.com/dotnet/aspnet:8.0
  WORKDIR /app
  COPY --from=build /app/out .
  ENV ASPNETCORE_URLS=http://+:10000
  ENV ASPNETCORE_ENVIRONMENT=Production
  EXPOSE 10000
  ENTRYPOINT ["dotnet", "ChatApp.dll"]