FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["UserService/UserService.csproj", "UserService/"]
RUN dotnet restore "UserService/UserService.csproj"

COPY UserService/. UserService/
RUN dotnet publish "UserService/UserService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS run
WORKDIR /app
EXPOSE 5002

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
