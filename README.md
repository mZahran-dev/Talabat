# Talabat API

A comprehensive food delivery platform API built with ASP.NET Core 7, implementing Onion Architecture and modern software architecture patterns.

## 📋 Overview

Talabat is a RESTful API that powers a food delivery platform, providing endpoints for managing restaurants, menus, orders, customers, and delivery operations. The project demonstrates Onion Architecture principles, repository pattern, and Domain-Driven Design (DDD).

## ✨ Features

### Core Functionality
- **User Management**
  - Customer registration and authentication
  - JWT token-based authorization
  - Role-based access control (Customer, Restaurant Owner, Admin)
  - User profile management
  - 
- **Order Management**
  - Shopping cart functionality
  - Order placement and tracking
  - Order status updates
  - Order history

- **Payment Integration**
  - Multiple payment methods support
  - Secure payment processing
  - Payment history and receipts

- **Delivery Management**
  - Delivery tracking
  - Delivery address management
  - Estimated delivery times

## 🏗️ Architecture

This project follows **Onion Architecture** with the following layers:

### Onion Architecture Layers (Inside-Out)

1. **Domain Layer (Core)**: 
   - Entities and value objects
   - Domain events
   - Interfaces (repository contracts)
   - Business rules and domain logic
   - Independent of any external concerns

2. **Application Layer (Service)**:
   - Application services and business logic
   - DTOs and mapping profiles
   - Interfaces for infrastructure services
   - CQRS patterns (if applicable)
   - Depends only on Domain layer

3. **Infrastructure Layer**:
   - Data access implementation (EF Core)
   - Repository implementations
   - External service integrations
   - Caching, logging, email services
   - Depends on Domain and Application layers

4. **Presentation Layer (API)**:
   - Controllers and endpoints
   - Middleware
   - API configuration
   - Dependency injection setup
   - Depends on all inner layers

### Key Principles
- **Dependency Inversion**: Inner layers don't depend on outer layers
- **Separation of Concerns**: Each layer has distinct responsibilities
- **Testability**: Core business logic isolated and easily testable
- **Flexibility**: Easy to swap implementations without affecting core logic

### Design Patterns Used
- Repository Pattern
- Unit of Work Pattern
- Specification Pattern
- Dependency Injection
- AutoMapper for object mapping

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core 7.0
- **Architecture**: Onion Architecture
- **ORM**: Entity Framework Core 7.0
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Documentation**: Swagger/OpenAPI
- **Caching**: Redis (optional)
- **Validation**: FluentValidation
- **Mapping**: AutoMapper

## 📦 Installation

### Prerequisites

- .NET 7.0 SDK or higher
- SQL Server 2019 or higher
- Visual Studio 2022 / VS Code / Rider
- Redis (optional, for caching)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/mZahran-dev/Talabat.git
   cd Talabat
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update the database connection string**
   
   Open `appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=TalabatDB;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run --project Talabat.API
   ```

6. **Access Swagger documentation**
   
   Navigate to: `https://localhost:5001/swagger`

## 🗂️ Project Structure

```
Talabat/
├── Talabat.Domain/               # Domain Layer (Core - Innermost)
│   ├── Entities/                 # Domain entities
│   ├── Interfaces/               # Repository interfaces
│   ├── Specifications/           # Specification pattern
│   └── Common/                   # Base entities and value objects
│
├── Talabat.Application/          # Application Layer (Service)
│   ├── Services/                 # Application services
│   ├── Interfaces/               # Service interfaces
│   ├── DTOs/                     # Data transfer objects
│   ├── Mappings/                 # AutoMapper profiles
│   └── Specifications/           # Business specifications
│
├── Talabat.Infrastructure/       # Infrastructure Layer
│   ├── Data/                     # DbContext and configurations
│   ├── Repositories/             # Repository implementations
│   ├── Identity/                 # Identity configuration
│   ├── Services/                 # External service implementations
│   └── Caching/                  # Caching implementation
│
└── Talabat.API/                  # Presentation Layer (Outermost)
    ├── Controllers/              # API controllers
    ├── Errors/                   # Error handling
    ├── Extensions/               # Dependency injection & services
    ├── Middleware/               # Custom middleware
    └── Program.cs                # Application entry point
```

### Dependency Flow (Onion Architecture)
```
API Layer → Infrastructure Layer → Application Layer → Domain Layer
    ↓              ↓                      ↓                ↓
(Depends on all) (Depends on       (Depends on      (No dependencies
                 App & Domain)      Domain only)     - Pure business logic)
```

## 💾 Database Schema

### Main Entities

- **User**: Customer and restaurant owner information
- **Product**: Food items and menu products
- **Category**: Product categorization
- **Order**: Customer orders
- **OrderItem**: Individual items in an order
- **DeliveryAddress**: Customer delivery addresses
- **Payment**: Payment transaction records

## 🔌 API Endpoints

### Authentication
```
POST   /api/auth/register        # Register new user
POST   /api/auth/login           # User login
GET    /api/auth/current-user    # Get current user
```

### Restaurants
```
GET    /api/restaurants          # Get all restaurants
GET    /api/restaurants/{id}     # Get restaurant by ID
POST   /api/restaurants          # Create restaurant (Admin)
PUT    /api/restaurants/{id}     # Update restaurant
DELETE /api/restaurants/{id}     # Delete restaurant
```

### Products
```
GET    /api/products             # Get all products
GET    /api/products/{id}        # Get product by ID
POST   /api/products             # Create product
PUT    /api/products/{id}        # Update product
DELETE /api/products/{id}        # Delete product
```

### Orders
```
GET    /api/orders               # Get user orders
GET    /api/orders/{id}          # Get order by ID
POST   /api/orders               # Create new order
PUT    /api/orders/{id}/status   # Update order status
```

### Basket
```
GET    /api/basket/{id}          # Get basket
POST   /api/basket               # Create/Update basket
DELETE /api/basket/{id}          # Delete basket
```

## 🔒 Security

- JWT token-based authentication
- Password hashing using ASP.NET Core Identity
- Role-based authorization
- CORS configuration
- Input validation and sanitization
- SQL injection prevention via EF Core
- API rate limiting (optional)


## 🚀 Deployment

### Docker Deployment

1. **Build Docker image**
   ```bash
   docker build -t talabat-api .
   ```

2. **Run container**
   ```bash
   docker run -p 8080:80 talabat-api
   ```

### Azure Deployment

1. Publish the application
   ```bash
   dotnet publish -c Release
   ```

2. Deploy to Azure App Service using Azure CLI or Visual Studio

## 📝 Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String",
    "Redis": "Your_Redis_Connection"
  },
  "JWT": {
    "Key": "Your_Secret_Key",
    "Issuer": "Talabat.API",
    "Audience": "Talabat.Client",
    "DurationInDays": 7
  },
  "Stripe": {
    "PublishableKey": "Your_Publishable_Key",
    "SecretKey": "Your_Secret_Key"
  }
}
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# coding conventions
- Write unit tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

## 📚 Learning Resources

This project demonstrates:
- onion Architecture implementation
- Repository and Unit of Work patterns
- Specification pattern for complex queries
- JWT authentication in .NET Core
- Entity Framework Core best practices
- RESTful API design principles

## 👤 Author

**mZahran-dev**

- GitHub: [@mZahran-dev](https://github.com/mZahran-dev)

## 🙏 Acknowledgments

- Inspired by Talabat food delivery platform
- Built as a learning project for .NET Core and onion Architecture


⭐ If you find this project useful, please consider giving it a star on GitHub!
