FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ballast.Api/Ballast.Api.csproj", "Ballast.Api/"]
RUN dotnet restore "Ballast.Api/Ballast.Api.csproj"
COPY . .
WORKDIR "/src/Ballast.Api"
RUN dotnet build "Ballast.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ballast.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ballast.Api.dll"]
