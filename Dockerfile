FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln .
COPY JobListing/*.csproj JobListing/
COPY JobListing.Common/*.csproj JobListing.Common/
COPY JobListing.Data/*.csproj JobListing.Data/
COPY Services/*.csproj Services/
COPY Models/*.csproj Models/
COPY JobListing.Test/*.csproj JobListing.Test/

RUN dotnet restore
COPY . .

FROM build AS publish
WORKDIR /src/JobListing
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

ENV ASPNETCORE_URL=http://*:$PORT
ENTRYPOINT ["dotnet", "JobListing.UI.dll"]
