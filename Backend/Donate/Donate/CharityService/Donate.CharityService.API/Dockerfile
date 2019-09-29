FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "CharityService/Donate.CharityService.API/Donate.CharityService.API.csproj"
COPY . .
WORKDIR "/src/CharityService/Donate.CharityService.API"
RUN dotnet build "Donate.CharityService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Donate.CharityService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Donate.CharityService.API.dll"]
