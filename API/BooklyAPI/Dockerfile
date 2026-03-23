FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BooklyAPI/BooklyAPI.csproj", "BooklyAPI/"]
COPY ["ApiApplication/ApiApplication.csproj", "ApiApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "BooklyAPI/BooklyAPI.csproj"

COPY . .
WORKDIR /src/BooklyAPI
RUN dotnet publish "BooklyAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5555
EXPOSE 5555

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BooklyAPI.dll"]
