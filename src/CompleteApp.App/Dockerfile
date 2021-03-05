#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/CompleteApp.App/CompleteApp.App.csproj", "src/CompleteApp.App/"]
RUN dotnet restore "src/CompleteApp.App/CompleteApp.App.csproj"
COPY . .
WORKDIR "/src/src/CompleteApp.App"
RUN dotnet build "CompleteApp.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompleteApp.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompleteApp.App.dll"]