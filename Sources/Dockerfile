#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#API
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /api
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["DTO/DTO_API.csproj", "DTO/"]
COPY ["EntityFramework/EntityFramework.csproj", "EntityFramework/"]
COPY ["DTO_EF/DTO_EF.csproj", "DTO_EF/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["StubLib/StubLib.csproj", "StubLib/"]
RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /api/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /api/publish /p:UseapiHost=false

FROM base AS final
WORKDIR /api
COPY --from=publish /api/publish .

RUN dotnet run

ENTRYPOINT ["echo", "API.dll"]