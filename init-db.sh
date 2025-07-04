#!/bin/bash

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready..."
sleep 30

# Run migrations
echo "Running Entity Framework migrations..."
dotnet ef migrations add UpdateModel --project InsurantSalesTelegramBot/InsurantSalesTelegramBot.csproj
dotnet ef database update --project InsurantSalesTelegramBot/InsurantSalesTelegramBot.csproj

echo "Database initialization completed!"
