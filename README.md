JobPortal – Web API (.NET Core)
A clean, modular, scalable ASP.NET Core Web API backend for a Job Portal system. This API powers the Angular frontend and provides endpoints for authentication, job management, applications, interviews, and dashboard analytics.

🚀 Features
🔐 Authentication
JWT‑based login

Role‑based authorization

Secure endpoints

🏢 Company Management
Add, update, list companies

Manage company members

💼 Job Management
CRUD operations for job posts

Job categories, skills, qualifications

📝 Application Management
Submit applications

Update application status

View application details

👥 Interview Management
Schedule interviews

Update or cancel interviews

Interview listing

🛠 Tech Stack

| Technology | Purpose |
| --- | --- |
| **ASP.NET Core Web API** | Backend framework |
| **Entity Framework Core** | ORM |
| **SQL Server** | Database |
| **JWT Authentication** | Security |
| **AutoMapper** | DTO mapping |
| **Swagger** | API documentation |

## 📁 Project Structure

```
JobPortal_WebApi/
 ├── Controllers/
 ├── Models/
 ├── DTOs/
 ├── Services/
 ├── Repositories/
 ├── Migrations/
 ├── appsettings.json
 └── Program.cs
```

▶️ How to Run the API
1️⃣ Restore dependencies

dotnet restore

2️⃣ Update database

dotnet ef database update

3️⃣ Run the API

dotnet run

4️⃣ Swagger UI

https://localhost:5001/swagger

🔗 Frontend
This API is consumed by:

👉 JobPortal_Angular

🌐 Deployment Options
Azure App Service

AWS Elastic Beanstalk

Docker + Kubernetes

IIS Hosting

👩‍💻 Author
Neethu K J  
.NET & Angular Developer – Dubai, UAE
