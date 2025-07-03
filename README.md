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

