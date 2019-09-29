FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "FundService/Donate.FundService.TransactionFeed/Donate.FundService.TransactionFeed.csproj"
COPY . .
WORKDIR "/src/FundService/Donate.FundService.TransactionFeed"
RUN dotnet build "Donate.FundService.TransactionFeed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Donate.FundService.TransactionFeed.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Donate.FundService.TransactionFeed.dll"]