#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Api/WebApi/Sozluk.Api.WebApi/Sozluk.Api.WebApi.csproj", "src/Api/WebApi/Sozluk.Api.WebApi/"]
COPY ["src/Api/Infrastructure/Sozluk.Infrastructure.Persistence/Sozluk.Infrastructure.Persistence.csproj", "src/Api/Infrastructure/Sozluk.Infrastructure.Persistence/"]
COPY ["src/Api/Core/Sozluk.Api.Application/Sozluk.Api.Application.csproj", "src/Api/Core/Sozluk.Api.Application/"]
COPY ["src/Api/Core/Sozluk.Api.Domain/Sozluk.Api.Domain.csproj", "src/Api/Core/Sozluk.Api.Domain/"]
COPY ["src/Common/Sozluk.Common/Sozluk.Common.csproj", "src/Common/Sozluk.Common/"]
RUN dotnet restore "src/Api/WebApi/Sozluk.Api.WebApi/Sozluk.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/Sozluk.Api.WebApi"
RUN dotnet build "Sozluk.Api.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sozluk.Api.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sozluk.Api.WebApi.dll"]