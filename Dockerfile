#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV HTTP_PROXY http://proxylab.ucab.edu.ve:3128
ENV HTTPS_PROXY http://proxylab.ucab.edu.ve:3128

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["computación en la nube - práctica 2C.csproj", "."]
RUN dotnet restore "./computación en la nube - práctica 2C.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "computación en la nube - práctica 2C.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "computación en la nube - práctica 2C.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "computación en la nube - práctica 2C.dll"]
