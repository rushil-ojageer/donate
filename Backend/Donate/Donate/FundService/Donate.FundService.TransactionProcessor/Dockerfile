FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "FundService/Donate.FundService.TransactionProcessor/Donate.FundService.TransactionProcessor.csproj"
COPY . .
WORKDIR "/src/FundService/Donate.FundService.TransactionProcessor"
RUN dotnet build "Donate.FundService.TransactionProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Donate.FundService.TransactionProcessor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Donate.FundService.TransactionProcessor.dll"]