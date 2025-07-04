FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY InsurantSalesTelegramBot.sln .

COPY InsurantSalesTelegramBot/InsurantSalesTelegramBot.csproj ./InsurantSalesTelegramBot/
COPY InsurantSales.Application/InsurantSales.Application.csproj ./InsurantSales.Application/
COPY InsurantSales.Domain/InsurantSales.Domain.csproj ./InsurantSales.Domain/
COPY InsurantSales.DataAccess/InsurantSales.DataAccess.csproj ./InsurantSales.DataAccess/

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore

RUN dotnet publish InsurantSalesTelegramBot/InsurantSalesTelegramBot.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "InsurantSalesTelegramBot.dll"]
