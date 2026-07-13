Student Career Management System

A backend RESTful API built with ASP.NET Core (.NET 8) and C#, designed to manage student records, authentication, and career/job matching features using Clean Architecture principles.

📌 Overview

This project provides a complete backend system for managing students and connecting them with career opportunities. It includes secure authentication, full CRUD operations for student data, and career/job matching functionality — all built with industry-standard patterns and practices.

✨ Features


🔐 Authentication & Authorization — Secure user login/registration with role-based access control
🎓 Student Management — Full CRUD operations (Create, Read, Update, Delete) for student records
💼 Career/Job Matching — Match students with relevant career opportunities
🗄️ Data Persistence — SQL Server integration via Entity Framework Core


🛠️ Tech Stack

CategoryTechnologyLanguageC#FrameworkASP.NET Core (.NET 8)DatabaseSQL ServerORMEntity Framework CoreAuthenticationASP.NET Core IdentityAPI TestingPostman / Swagger

🏗️ Architecture

This project follows Clean Architecture principles, separating concerns into distinct layers:

├── Domain           # Entities, enums, core business rules
├── Application       # Use cases, DTOs, interfaces
├── Infrastructure     # EF Core, repositories, external services
└── API                 # Controllers, middleware, startup config

🚀 Getting Started

Prerequisites


.NET 8 SDK
SQL Server (LocalDB or full instance)
Visual Studio 2022 / VS Code


Installation


Clone the repository


bash   git clone https://github.com/Mr-Mudasir-Dev/Student-Career-Management-System.git


Navigate to the project directory


bash   cd Student-Career-Management-System


Update the connection string in appsettings.json with your SQL Server details
Apply database migrations


bash   dotnet ef database update


Run the application


bash   dotnet run


Open Swagger UI (usually at https://localhost:5001/swagger) to explore and test the API endpoints


📖 API Endpoints (Sample)

MethodEndpointDescriptionPOST/api/auth/registerRegister a new userPOST/api/auth/loginLogin and get JWT tokenGET/api/studentsGet all studentsGET/api/students/{id}Get student by IDPOST/api/studentsCreate a new studentPUT/api/students/{id}Update student detailsDELETE/api/students/{id}Delete a student

📂 Project Status

🚧 Actively under development — new features and improvements are being added regularly as part of ongoing learning in backend development.

👤 Author

Mudasir


GitHub: @Mr-Mudasir-Dev


📄 License

This project is open source and available for learning purposes.
