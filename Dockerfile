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

# Copy solution file
COPY Cads.Cds.sln .

# Copy full src
COPY src/ ./src/

# Remove unwanted projects from the solution before restore
RUN dotnet sln Cads.Cds.sln remove docker-compose.dcproj || true
RUN dotnet sln Cads.Cds.sln list | grep "Tests" | xargs -I {} dotnet sln Cads.Cds.sln remove "{}" || true

# Restore solution
RUN dotnet restore -r linux-${TARGETARCH} -v n

# Build
RUN dotnet build src/Cads.Cds/Cads.Cds.csproj \
    -c ${BUILD_CONFIGURATION} \
    -o /app/build \
    -r linux-${TARGETARCH} \
    --no-restore

# Publish
FROM build AS publish
RUN dotnet publish src/Cads.Cds/Cads.Cds.csproj \
    -c ${BUILD_CONFIGURATION} \
    -o /app/publish \
    -r linux-${TARGETARCH} \
    --no-restore \
    /p:UseAppHost=false

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Final production image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8085
ENTRYPOINT ["dotnet", "Cads.Cds.dll"]
