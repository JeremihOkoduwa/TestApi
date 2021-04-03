#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TestApi/TestApi.csproj", "TestApi/"]
COPY ["TestRepo/Test.Repo/Test.Repo.csproj", "TestRepo/Test.Repo/"]
COPY ["TestCore/Test.Core/Test.Core.csproj", "TestCore/Test.Core/"]
RUN dotnet restore "TestApi/TestApi.csproj"
COPY . .
WORKDIR "/src/TestApi"
RUN dotnet build "TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApi.dll"]