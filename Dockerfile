#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Erfa.ProductionManagement.Api/Erfa.ProductionManagement.Api.csproj", "Erfa.ProductionManagement.Api/"]
COPY ["Erfa.ProductionManagement.Persistance/Erfa.ProductionManagement.Persistance.csproj", "Erfa.ProductionManagement.Persistance/"]
COPY ["Erfa.ProductionManagement.Application/Erfa.ProductionManagement.Application.csproj", "Erfa.ProductionManagement.Application/"]
COPY ["Erfa.ProductionManagement.Domain/Erfa.ProductionManagement.Domain.csproj", "Erfa.ProductionManagement.Domain/"]
RUN dotnet restore "Erfa.ProductionManagement.Api/Erfa.ProductionManagement.Api.csproj"
COPY . .
WORKDIR "/src/Erfa.ProductionManagement.Api"
RUN dotnet build "Erfa.ProductionManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Erfa.ProductionManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Erfa.ProductionManagement.Api.dll"]