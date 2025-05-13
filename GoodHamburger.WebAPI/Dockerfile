FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GoodHamburger.WebAPI/GoodHamburger.WebAPI.csproj", "GoodHamburger.WebAPI/"]
RUN dotnet restore "GoodHamburger.WebAPI/GoodHamburger.WebAPI.csproj"
COPY . .
WORKDIR "/src/GoodHamburger.WebAPI"
RUN dotnet build "GoodHamburger.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodHamburger.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodHamburger.WebAPI.dll"]
