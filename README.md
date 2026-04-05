# University Management System

A comprehensive ASP.NET Core MVC application designed for managing university data including students, departments, courses, and instructors. This project demonstrates modern web development practices with robust authentication, data validation, and error handling.

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Database Configuration](#database-configuration)
- [Authentication](#authentication)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Middleware & Filters](#middleware--filters)
- [Validation](#validation)
- [Logging](#logging)
- [Contributing](#contributing)
- [License](#license)

## Features

### Core Functionality

- **Student Management**: Complete CRUD operations for student records with image upload capability
- **Department Management**: Manage university departments with location validation
- **Course Management**: Create and manage courses with topics and grading information
- **Instructor Management**: Handle instructor data and course assignments
- **User Authentication**: Secure login/registration with role-based access control

### Advanced Features

- **Google OAuth Integration**: Social login using Google accounts
- **Session Management**: Server-side session storage for user data
- **File Upload**: Secure image upload for student profiles
- **Custom Validation**: Business logic validation with custom attributes
- **Error Handling**: Comprehensive error handling with custom exceptions
- **Logging**: Structured logging with Serilog
- **Data Mapping**: Object-to-object mapping using Mapster

### Security & Performance

- **Role-based Authorization**: Admin and user role separation
- **Custom Filters**: Request/response filtering and authorization
- **Middleware Pipeline**: Custom middleware for logging and error handling
- **Input Validation**: Client and server-side validation
- **Anti-forgery Protection**: CSRF protection on forms

## Technology Stack

### Backend

- **Framework**: ASP.NET Core MVC 10.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity with Google OAuth
- **ORM**: Entity Framework Core 10.0
- **Logging**: Serilog
- **Mapping**: Mapster 10.0

### Frontend

- **Views**: Razor Pages
- **Styling**: Bootstrap (via wwwroot)
- **JavaScript**: Vanilla JS with jQuery

### Development Tools

- **IDE**: Visual Studio 2022
- **Version Control**: Git
- **Package Manager**: NuGet

## Project Structure

```
WebApplication1/
├── Controllers/           # MVC Controllers
│   ├── AccountController.cs
│   ├── CourseController.cs
│   ├── DepartmentController.cs
│   ├── HomeController.cs
│   ├── StateManagementController.cs
│   └── StudentController.cs
├── Models/               # Entity Models
│   ├── ApplicationUser.cs
│   ├── Course.cs
│   ├── Department.cs
│   ├── Instructor.cs
│   ├── Student.cs
│   ├── StudCourse.cs
│   └── InsCourse.cs
├── ViewModels/           # View Models for forms
├── Views/               # Razor Views
├── Context/             # Database Context
│   └── AppDbContext.cs
├── Repositories/        # Repository Pattern
├── Filters/             # Action Filters
├── Middlewares/         # Custom Middleware
├── Validators/          # Custom Validation Attributes
├── Utils/               # Utility Classes
├── Migrations/          # EF Core Migrations
├── wwwroot/             # Static Files
├── appsettings.json     # Configuration
└── Program.cs           # Application Entry Point
```

## Prerequisites

- .NET 10.0 SDK
- SQL Server (Express or higher)
- Visual Studio 2022 (recommended) or VS Code
- Git

## Installation & Setup

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd WebApplication1
   ```

2. **Restore NuGet packages**

   ```bash
   dotnet restore
   ```

3. **Build the project**

   ```bash
   dotnet build
   ```

4. **Configure Database** (see Database Configuration section)

5. **Run the application**
   ```bash
   dotnet run
   ```

The application will be available at `https://localhost:5001` (or similar port).

## Database Configuration

### Connection String

Update `appsettings.json` with your SQL Server connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MVCD1;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### Database Migration

Apply existing migrations:

```bash
dotnet ef database update
```

### Create New Migration (if needed)

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Authentication

### User Registration/Login

- Standard email/password authentication
- Google OAuth integration
- Role-based access (Admin/User)

### Admin Features

- Create/Edit/Delete students, departments, and courses
- Access restricted to users with "Admin" role

### Google OAuth Setup

Configure Google OAuth credentials in `appsettings.json`:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "your-client-id",
      "ClientSecret": "your-client-secret"
    }
  }
}
```

## Usage

### Student Management

1. Navigate to `/Student/GetAll` to view all students
2. Admin users can create new students via `/Student/New`
3. Upload student profile images (supported formats: images only)
4. Edit student details including department assignment

### Department Management

1. View departments at `/Department/GetAll`
2. Create new departments with location validation (USA/EG only)
3. Edit department information

### Course Management

1. Browse courses at `/Course/GetAll`
2. Add courses with topics (comma-separated)
3. Set grade requirements and minimum degrees

### State Management Demo

- `/StateManagement/SetSession` - Demonstrate session storage
- `/StateManagement/SetLanguageCookie` - Demonstrate cookie storage

## API Endpoints

### Students

- `GET /Student/GetAll` - List all students
- `GET /Student/Details/{id}` - Student details with department and courses
- `GET/POST /Student/New` - Create new student (Admin only)
- `GET/POST /Student/Edit/{id}` - Edit student (Admin only)
- `POST /Student/CheckEmailUnique` - Validate email uniqueness

### Departments

- `GET /Department/GetAll` - List departments
- `GET /Department/Details/{id}` - Department details with students
- `GET/POST /Department/New` - Create department (Admin only)
- `GET/POST /Department/Edit/{id}` - Edit department (Admin only)

### Courses

- `GET /Course/GetAll` - List courses
- `GET /Course/Details/{id}` - Course details
- `GET/POST /Course/New` - Create course (Admin only)
- `GET/POST /Course/Edit/{id}` - Edit course (Admin only)

### Authentication

- `GET/POST /Account/Register` - User registration
- `GET/POST /Account/Login` - User login
- `POST /Account/Logout` - User logout
- `GET /Account/GoogleLogin` - Google OAuth login

## Middleware & Filters

### Custom Middleware

- **RequestLoggingMiddleware**: Logs request/response details with timing
- **GlobalErrorHandlerMiddleware**: Catches exceptions and returns JSON error responses

### Action Filters

- **ExceptionHandlerFilter**: Handles MVC exceptions and renders error views
- **AddInfoToResponse**: Adds custom headers to responses
- **AuthorizeFilter**: Custom authorization based on request headers
- **CheckLocation**: Validates department location (USA/EG)

## Validation

### Built-in Validation

- Data annotations on models (Required, Range, EmailAddress, etc.)
- Regular expressions for phone numbers and locations

### Custom Validation Attributes

- **UniqueAttribute**: Ensures email uniqueness across students
- **DepartIsExistsAttribute**: Validates department existence
- **AllowedExtensionsAttribute**: File upload validation
- **MaxFileSizeAttribute**: File size limits
- **MinValueAttribute**: Minimum value validation

## Logging

### Serilog Configuration

- Console logging for development
- File logging with daily rotation (`logs/myapp-yyyyMMdd.txt`)
- Structured logging with request details

### Log Levels

- Information: Request/response logging
- Warning: Application warnings
- Error: Exception details

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

---

**Developed by**: ESLAM TAREK
**Application Name**: MyFirst MVC App
