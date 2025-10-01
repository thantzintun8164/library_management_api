# 📚 Library Management System API
A modular and scalable **Library Management System API** built with **ASP.NET Core** following **Clean Architecture** and **CQRS with MediatR**.  
This project provides a robust backend for managing **users, books, authors, categories, borrowing records, and authentication**.  

It integrates modern practices such as **Repository Pattern**, **Unit of Work**, **Result Pattern**, **Background Jobs**, **Caching**, and **Rate Limiting** to ensure performance, maintainability, and security.

---

##  Key Features

- **Clean Architecture** (API, Application, Domain, Infrastructure)  
- **CQRS + MediatR** with pipeline behaviors  
- **Caching (In-Memory)** with cache invalidation behaviors  
- **Repository Pattern** & **Unit of Work**
- **Result Pattern** for consistent responses  
- **AutoMapper** for DTO mapping  
- **FluentValidation** with automatic validation behaviors  
- **Hangfire** for background jobs  
- **Serilog** for structured logging  
- **Options Pattern** for configuration  
- **Centralized API Error Responses**
- **Custom exceptions** (`NotFound`, `Forbidden`, `Unauthorized`, `BadRequest`, `Validation`)  
- **SMTP Email Integration** (notifications, password reset, etc.)
- **Email Templates** for Forgot Password & Change Password notifications
- **File uploads & Download** (book files)  
- **Global Exception Middleware**  
- **Rate Limiting**
- **Data Seeding** for default roles, admin user, and sample data (books, authors, categories)  
- **Swagger / OpenAPI 3.0** documentation
- **Enhanced Swagger Documentation** with:
  - `Swashbuckle.AspNetCore.Annotations` for rich metadata  
  - Custom filters (e.g., hiding internal cache keys)  


---

##  Authentication & Account Management
- JWT authentication with **refresh tokens**  
- Role-based access control (Admin, User)  
- User registration, login, logout, revoke tokens  
- Forgot & reset password **via email**  
- Profile management (view & update)  
- Assign / remove roles  

---

##  Books
- Full CRUD operations  
- Borrow & return books  
- Availability management  
- File uploads (book file download/update)  
- Search & pagination  
- List available books  

---

##  Authors
- CRUD operations  
- Get authors with pagination  
- List books by author  

---

##  Categories
- CRUD operations  
- Pagination & search  
- Get related books & authors by category  

---

##  Borrow Records
- View all borrow records  
- Get records by user  
- Renew borrowed books  
- Track overdue & returned books  
- Paginated listing  

---

##  Tech Stack
- **ASP.NET Core 8** – Web API framework  
- **Entity Framework Core** – ORM for database access  
- **SQL Server** – Relational database  
- **Identity + JWT** – Authentication & Authorization  
- **MediatR (CQRS)** – Command/Query separation  
- **Hangfire** – Background jobs & scheduling  
- **Serilog** – Logging  
- **AutoMapper** – Object mapping  
- **FluentValidation** – Request validation  
- **In-Memory Cache** – Caching with invalidation behaviors  
- **Swagger / OpenAPI 3.0** – API documentation
- **Apidog** for test endpoint
---

#  API Endpoints

```http
#  Auth
POST   /api/Auth/register
POST   /api/Auth/login
POST   /api/Auth/logout
POST   /api/Auth/refresh-token
POST   /api/Auth/revoke-token

#  Accounts
GET    /api/Accounts/profile
PUT    /api/Accounts/profile
GET    /api/Accounts/{id}
GET    /api/Accounts
GET    /api/Accounts/pagination
POST   /api/Accounts/change-password
POST   /api/Accounts/add-role
POST   /api/Accounts/remove-role
POST   /api/Accounts/forget-password
POST   /api/Accounts/reset-password

#  Authors
GET    /api/Authors
POST   /api/Authors
GET    /api/Authors/{id}
PUT    /api/Authors/{id}
DELETE /api/Authors/{id}
GET    /api/Authors/{authorName}/books
GET    /api/Authors/pagination

#  Books
GET    /api/Books
POST   /api/Books
GET    /api/Books/{id}
PUT    /api/Books/{id}
DELETE /api/Books/{id}
GET    /api/Books/search
GET    /api/Books/available
GET    /api/Books/{id}/download
GET    /api/Books/pagination
POST   /api/Books/{id}/borrow
POST   /api/Books/{id}/return
PATCH  /api/Books/{id}/availability
PATCH  /api/Books/{id}/file

#  BorrowBooks
GET    /api/BorrowBooks
GET    /api/BorrowBooks/{id}
GET    /api/BorrowBooks/user/{userId}
GET    /api/BorrowBooks/overdue
GET    /api/BorrowBooks/returned
GET    /api/BorrowBooks/pagination
PATCH  /api/BorrowBooks/{id}/renew

#  Categories
GET    /api/Categories
POST   /api/Categories
GET    /api/Categories/{id}
PUT    /api/Categories/{id}
DELETE /api/Categories/{id}
GET    /api/Categories/pagination
GET    /api/Categories/{categoryName}/books
GET    /api/Categories/{categoryName}/authors
```


