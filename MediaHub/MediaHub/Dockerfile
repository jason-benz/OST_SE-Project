#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediaHub/MediaHub.csproj", "MediaHub/"]
RUN dotnet restore "MediaHub/MediaHub.csproj"
RUN dotnet user-secrets set SendGridKey "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" --project "MediaHub/MediaHub/MediaHub.csproj"
COPY . .
WORKDIR "/src/MediaHub"
RUN dotnet build "MediaHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaHub.dll"]


