# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/uBee.Api/uBee.Api.csproj", "src/uBee.Api/"]
COPY ["src/uBee.Infrastructure/uBee.Infrastructure.csproj", "src/uBee.Infrastructure/"]
COPY ["src/uBee.Application/uBee.Application.csproj", "src/uBee.Application/"]
COPY ["src/uBee.Domain/uBee.Domain.csproj", "src/uBee.Domain/"]
COPY ["src/uBee.Shared/uBee.Shared.csproj", "src/uBee.Shared/"]
COPY ["src/uBee.Persistence/uBee.Persistence.csproj", "src/uBee.Persistence/"]
RUN dotnet restore "./src/uBee.Api/uBee.Api.csproj"
COPY . .
WORKDIR "/src/src/uBee.Api"
RUN dotnet build "./uBee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./uBee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uBee.Api.dll"]