FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["profile_API/profile_API.csproj", "profile_API/"]
COPY ["profile_Application/profile_Application.csproj", "profile_Application/"]
COPY ["profile_Core/profile_Core.csproj", "profile_Core/"]
COPY ["profile_Domain/profile_Domain.csproj", "profile_Domain/"]
COPY ["profile_MapperAndModel/profile_MapperAndModel.csproj", "profile_MapperAndModel/"]
COPY ["profile_DataAccess/profile_DataAccess.csproj", "profile_DataAccess/"]
COPY ["profile_Service/profile_Service.csproj", "profile_Service/"]
RUN dotnet restore "profile_API/profile_API.csproj"
COPY . .
WORKDIR "/src/profile_API"
RUN dotnet build "profile_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "profile_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "profile_API.dll"]
