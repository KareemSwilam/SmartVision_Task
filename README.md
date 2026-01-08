FougeraClub - SmartVision Task
A modern ASP.NET Core web application built with Clean Architecture principles, demonstrating separation of concerns, maintainability, and scalability.
🏗️ Clean Architecture Overview
This project follows Clean Architecture , which organizes code into layers with dependencies pointing inward. This approach ensures that business logic remains independent of external concerns like databases, frameworks, or UI.
Architecture Layers
┌─────────────────────────────────────────┐
│     FougeraClub (Presentation Layer)    │
│           MVC / Controller              │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│  FougeraClub.Services (Application)     │
│    Business Logic & Use Cases           │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│    FougeraClub.Core (Domain Layer)      │
│   Entities, Interfaces, Abstractions    │
└─────────────────────────────────────────┘
                    ↑
┌─────────────────────────────────────────┐
│ FougeraClub.Infrastructure (Data Layer) │
│  Database, External Services, Repos     │
└─────────────────────────────────────────┘
📁 Project Structure
1. FougeraClub.Core (Domain Layer)
The innermost layer containing the core business logic and domain models. This layer has no dependencies on other projects.
Contains:

Entities: Core business objects and domain models
Interfaces: Repository and service abstractions
Domain Events: Events that represent business occurrences



2. FougeraClub.Infrastructure (Infrastructure Layer)
Implements interfaces defined in the Core layer, handling data persistence and external services.
Contains:

Data Context: Entity Framework Core DbContext
Repository Implementations: Concrete implementations of repository interfaces
Migrations: Database schema migrations

Responsibilities:

Database operations
Data access logic
Infrastructure concerns
External service communication

3. FougeraClub.Services (Application Layer)
Contains application-specific business rules and orchestrates the flow of data between layers.
Contains:

Service Implementations: Business logic services
DTOs (Data Transfer Objects): Data contracts for API responses
Mappers: Object-to-object mapping logic
Validators: Input validation logic
Services: Application-specific operations
Application Interfaces: Service contracts


4. FougeraClub (Presentation Layer)
The ASP.NET Core Web API project that serves as the entry point of the application.
Contains:

Controllers: API endpoints and request handling
Middleware: Request/response pipeline components
Filters: Cross-cutting concerns (authorization, exception handling)
Configuration: Startup configuration and dependency injection
API Models: Request/response models specific to the API
