FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestMaster.sln", "./"]
COPY ["src/TestMaster.Api/TestMaster.Api.csproj", "src/TestMaster.Api/"]
COPY ["src/TestMaster.Core/TestMaster.Core.csproj", "src/TestMaster.Core/"]
COPY ["src/TestMaster.Infrastructure/TestMaster.Infrastructure.csproj", "src/TestMaster.Infrastructure/"]
COPY ["src/TestMaster.Tests/TestMaster.Tests.csproj", "src/TestMaster.Tests/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/src/TestMaster.Api"
RUN dotnet build "TestMaster.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMaster.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMaster.Api.dll"] 