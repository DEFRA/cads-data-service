# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Add curl to template.
# CDP PLATFORM HEALTHCHECK REQUIREMENT
RUN apt update && \
    apt install curl -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Build stage image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH=x64
ENV BUILD_CONFIGURATION=${BUILD_CONFIGURATION}
WORKDIR /src

# COPY ["Directory.Build.props", "."]
# COPY ["Directory.Build.targets", "."]

# Cads.Cds
COPY ["src/Cads.Cds/Cads.Cds.csproj", "Cads.Cds/"]

# Cads.Cds.ApiSurface
COPY ["src/ApiSurface/Cads.Cds.ApiSurface/Cads.Cds.ApiSurface.csproj", "Cads.Cds.ApiSurface/"]

# BuildingBlocks
COPY ["src/BuildingBlocks/Cads.Cds.BuildingBlocks.Infrastructure/Cads.Cds.BuildingBlocks.Infrastructure.csproj", "Cads.Cds.BuildingBlocks.Infrastructure/"]
COPY ["src/BuildingBlocks/Cads.Cds.BuildingBlocks.Application/Cads.Cds.BuildingBlocks.Application.csproj", "Cads.Cds.BuildingBlocks.Application/"]
COPY ["src/BuildingBlocks/Cads.Cds.BuildingBlocks.Core/Cads.Cds.BuildingBlocks.Core.csproj", "Cads.Cds.BuildingBlocks.Core/"]

# Modules
## Cads.Cds.Api
COPY ["src/Modules/Cads.Cds.Api/Cads.Cds.Api/Cads.Cds.Api.csproj", "Cads.Cds.Api/"]
COPY ["src/Modules/Cads.Cds.Api/Cads.Cds.Api.Infrastructure/Cads.Cds.Api.Infrastructure.csproj", "Cads.Cds.Api.Infrastructure/"]
COPY ["src/Modules/Cads.Cds.Api/Cads.Cds.Api.Application/Cads.Cds.Api.Application.csproj", "Cads.Cds.Api.Application/"]
COPY ["src/Modules/Cads.Cds.Api/Cads.Cds.Api.Core/Cads.Cds.Api.Core.csproj", "Cads.Cds.Api.Core/"]

## Cads.Cds.Ingester
COPY ["src/Modules/Cads.Cds.Ingester/Cads.Cds.Ingester/Cads.Cds.Ingester.csproj", "Cads.Cds.Ingester/"]
COPY ["src/Modules/Cads.Cds.Ingester/Cads.Cds.Ingester.Infrastructure/Cads.Cds.Ingester.Infrastructure.csproj", "Cads.Cds.Ingester.Infrastructure/"]
COPY ["src/Modules/Cads.Cds.Ingester/Cads.Cds.Ingester.Application/Cads.Cds.Ingester.Application.csproj", "Cads.Cds.Ingester.Application/"]
COPY ["src/Modules/Cads.Cds.Ingester/Cads.Cds.Ingester.Core/Cads.Cds.Ingester.Core.csproj", "Cads.Cds.Ingester.Core/"]

## Cads.Cds.MiBff
COPY ["src/Modules/Cads.Cds.MiBff/Cads.Cds.MiBff/Cads.Cds.MiBff.csproj", "Cads.Cds.MiBff/"]
COPY ["src/Modules/Cads.Cds.MiBff/Cads.Cds.MiBff.Infrastructure/Cads.Cds.MiBff.Infrastructure.csproj", "Cads.Cds.MiBff.Infrastructure/"]
COPY ["src/Modules/Cads.Cds.MiBff/Cads.Cds.MiBff.Application/Cads.Cds.MiBff.Application.csproj", "Cads.Cds.MiBff.Application/"]
COPY ["src/Modules/Cads.Cds.MiBff/Cads.Cds.MiBff.Core/Cads.Cds.MiBff.Core.csproj", "Cads.Cds.MiBff.Core/"]

## Cads.Cds.StorageBridge
COPY ["src/Modules/Cads.Cds.StorageBridge/Cads.Cds.StorageBridge/Cads.Cds.StorageBridge.csproj", "Cads.Cds.StorageBridge/"]
COPY ["src/Modules/Cads.Cds.StorageBridge/Cads.Cds.StorageBridge.Infrastructure/Cads.Cds.StorageBridge.Infrastructure.csproj", "Cads.Cds.StorageBridge.Infrastructure/"]
COPY ["src/Modules/Cads.Cds.StorageBridge/Cads.Cds.StorageBridge.Application/Cads.Cds.StorageBridge.Application.csproj", "Cads.Cds.StorageBridge.Application/"]
COPY ["src/Modules/Cads.Cds.StorageBridge/Cads.Cds.StorageBridge.Core/Cads.Cds.StorageBridge.Core.csproj", "Cads.Cds.StorageBridge.Core/"]

RUN dotnet restore "Cads.Cds/Cads.Cds.csproj" -r linux-${TARGETARCH} -v n

COPY ["src/", "."]

FROM build AS publish
ARG TARGETARCH=x64
WORKDIR "/src/Cads.Cds"
RUN dotnet publish "Cads.Cds.csproj" -v n -c ${BUILD_CONFIGURATION} -o /app/publish -r linux-${TARGETARCH} --no-restore /p:UseAppHost=false

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Final production image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8085
ENTRYPOINT ["dotnet", "Cads.Cds.dll"]
