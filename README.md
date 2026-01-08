FougeraClub - SmartVision
A modern ASP.NET Core web application built with Clean Architecture principles. This project focuses on separation of concerns, maintainability, and horizontal scalability.

🏗️ Clean Architecture Overview
This project follows the Clean Architecture pattern, where dependencies point inward. This ensures the core business logic remains decoupled from external frameworks, databases, and UI implementations.

Dependency Flow
Presentation → Application → Domain

Infrastructure → Domain

📁 Project Structure & Layers
1. FougeraClub.Core (Domain Layer)
The innermost layer containing the core business logic. It is independent of all other layers.

Entities: Core business objects and domain models.

Interfaces: Abstractions for repositories and services.

Domain Events: Logic representing specific business occurrences.

2. FougeraClub.Services (Application Layer)
Orchestrates the flow of data and implements application-specific business rules.

Service Implementations: Core application logic.

DTOs (Data Transfer Objects): Contracts for data exchange.

Mappers: Logic for object-to-object mapping .

Validators: Input and business rule validation.

3. FougeraClub.Infrastructure (Infrastructure Layer)
Handles external concerns and implements the interfaces defined in the Core layer.

Data Context: Entity Framework Core DbContext.

Repositories: Concrete implementations of data access logic.

Migrations: Database schema version control.

External Services: Integrations with third-party APIs or tools.

4. FougeraClub (Presentation Layer)
The entry point of the application (ASP.NET Core Web API).

Controllers: RESTful API endpoints.

Middleware: Custom logic for the request/response pipeline.

Filters: Authorization, logging, and global exception handling.

Configuration: Dependency Injection (DI) registration and app settings.
