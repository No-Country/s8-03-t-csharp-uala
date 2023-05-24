#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UalaReplicaProject/UalaReplicaProject.csproj", "UalaReplicaProject/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
RUN dotnet restore "UalaReplicaProject/UalaReplicaProject.csproj"
COPY . .
WORKDIR "/src/UalaReplicaProject"
RUN dotnet build "UalaReplicaProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UalaReplicaProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UalaReplicaProject.dll"]