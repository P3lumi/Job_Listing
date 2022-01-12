FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY JobListing/*.csproj JobListing/
COPY JobListing.Common/*.csproj JobListing.Common/
COPY JobListing.Data/*.csproj JobListing.Data/
COPY Services/*.csproj Services/
COPY Models/*.csproj Models/

FROM build AS publish
WORKDIR /src/JobListing
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "JobListing.UI.dll"]
