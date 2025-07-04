# Car Insurance Sales Telegram Bot

Built using .net 8, CQRS with MediatR, TelegramBot API, OpenAI API, Mindee API for document text extraction (ID and car documents).

---

# Features

- Sends a welcome message when the bot starts.
- Uses OpenAI to answer general user questions.
- Asks the user to upload ID document and car document.
- Extracts key information from uploaded documents using Mindee API (this sends unauthorized status code so that used faked JSON response in Mindee style).
- Sends extracted information to the user for confirmation (Yes/No buttons).
- Saves the original documents to the database.
- Detects new users and adds them to the database on first contact.

---

# Technologies & Architecture

| Layer                          | Technology                              |
|---------------------------------|-----------------------------------------|
| **Application Layer**           | MediatR, CQRS, Service Interfaces, Mindee API integration, OpenAI API, Telegram Service  |
| **Domain Layer**                | Entities  |
| **DataAccess Layer**            | Repository, UOW                   |


---

# How It Works (Flow)

1. User sends /start command
   - Bot greets and checks if the user exists in the database.
   - If not, adds user based on Telegram info.

2. User is asked to upload two documents one by one:
   - ID Document
   - Car Document

3. For each document:
   - The document is saved in the local file + saved to the DB.
   - The file is processed with Mindee API to extract text. The extracted fields should be saved to the local cache with related document info.
   - The extracted fields are displayed to the user for confirmation (Yes/No).
   - On a "Yes" answer, the extracted fields should be saved to the DB from cache.

5. Any other message: 
   → Passed to OpenAI for a smart reply.

6. Deployment process:
1. Create .env file from template:
      cp COPY.env .env
   

2. Fill in the required environment variables in .env file:
   # Database Configuration
   DATABASE_SERVER=sql1
   DATABASE_PORT=1433
   DATABASE_NAME=InsurantSalesDB
   DATABASE_USER=sa
   MSSQL_SA_PASSWORD=YourStrongP@ssw0rd123

   # API Keys
   TELEGRAM_BOT_TOKEN=your_telegram_bot_token_here
   OPENAI_API_KEY=your_openai_api_key_here
   MINDEE_API_KEY=your_mindee_api_key_here
   

3. Build the main application image:
   docker build -t telegram-bot:latest -f Dockerfile .

   Build the migrations image:
   docker build -t telegram-bot-migration:latest -f Dockerfile.migrations .

4. Start all services:
   docker-compose up -d

This will:
1. Start SQL Server container
2. Run database migrations
3. Start the Telegram bot application
