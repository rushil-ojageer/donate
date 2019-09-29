FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "DonorService/Donate.DonorService.IntegrationWorker/Donate.DonorService.IntegrationWorker.csproj"
COPY . .
WORKDIR "/src/DonorService/Donate.DonorService.IntegrationWorker"
RUN dotnet build "Donate.DonorService.IntegrationWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Donate.DonorService.IntegrationWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Donate.DonorService.IntegrationWorker.dll"]