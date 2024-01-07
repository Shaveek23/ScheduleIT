#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["ScheduleIT/ScheduleIT.csproj", "ScheduleIT/"]
RUN dotnet restore "./ScheduleIT/./ScheduleIT.csproj"
COPY . .
WORKDIR "./ScheduleIT"
RUN dotnet build "ScheduleIT.csproj" -c $BUILD_CONFIGURATION -o /app/build
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef --version 3.1.1

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ScheduleIT.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleIT.dll"]


#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#COPY ["ScheduleIT.Domain/ScheduleIT.Domain.csproj", "ScheduleIT.Domain/"]
#COPY ["ScheduleIT.Contracts/ScheduleIT.Contracts.csproj", "ScheduleIT.Contracts/"]
#COPY ["ScheduleIT.Application/ScheduleIT.Application.csproj", "ScheduleIT.Application/"]
#COPY ["ScheduleIT.Infrastructure/ScheduleIT.Infrastructure.csproj", "ScheduleIT.Infrastructure/"]
#COPY ["ScheduleIT.Persistence/ScheduleIT.Persistence.csproj", "ScheduleIT.Persistence/"]
#COPY ["ScheduleIT/ScheduleIT.csproj", "ScheduleIT/"]
#RUN dotnet restore "ScheduleIT/ScheduleIT.csproj"
#
#COPY . .
#WORKDIR "/ScheduleIT"
#FROM build AS publish
#RUN dotnet publish "ScheduleIT.csproj" --no-restore -c Development -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ScheduleIT.dll"]