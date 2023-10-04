# Ballast API

## Project Solution Overview

The project follows a clean architecture pattern with a clear separation of concerns. Below are the main components of the solution:

1. **Ballast.Api**:
   - Contains the API controllers responsible for handling HTTP requests.
   - Includes filters and service configurations for request/response processing.

2. **Ballast.Application**:
   - Implements the CQRS (Command Query Responsibility Segregation) pattern using Mediatr.
   - Contains commands and queries for Products and User functionalities.
   - Provides services for password hashing (IPasswordHasher) and JWT token management (IJwtTokenService).
   - Defines ports/interfaces for data access, including IProductRepository and IUserRepository.

3. **Ballast.Domain**:
   - Contains entity classes that represent core business objects, such as User and Product.

4. **Ballast.MemorySql**:
   - It has the implementation of data repositories using Dapper with SQL Server.
   - Includes the SQL script necessary for database setup. https://github.com/betocastillo86/temp.ballast/blob/main/Ballast.Api/Ballast.MemorySql/database.sql
   - Note: Due to limitations on the Apple M1 architecture, running an in-memory database might not be feasible. Instead, Docker was used for database setup and management.

5. **Ballast.Application.Tests**:
   - Includes unit tests for the application layer, validating the correctness of various functionalities.
   - Note: Integration tests are not included at this time. However, a Postman collection is provided to facilitate API testing. https://github.com/betocastillo86/temp.ballast/blob/main/Ballast.postman_collection.json

This solution structure ensures a clean separation of concerns and maintainability of the codebase, following best practices in software architecture and design.


## User Stories

### User Story 1: Create a Project API following Clean Architecture

Description: As a developer, I want to create a web application API following Clean Architecture principles to ensure separation of concerns and maintainability.

Acceptance Criteria:

- Implement a solution structure that adheres to Clean Architecture principles, including clear separation of layers (Presentation, Application, Domain, and Infrastructure).
- Set up the necessary project files and dependencies to support the Clean Architecture.

### User Story 2: Create a User Entity and Endpoints for Registration and Login

Description: As a user, I want to be able to register and log in to the application.

Acceptance Criteria:

- Define a User entity with appropriate fields, including but not limited to username and password.
- Create API endpoints for user registration and login.
- Implement unit tests (UT) to ensure the functionality of user registration and login.

### User Story 3: Create a Product Entity and Endpoints for CRUD Operations

Description: As an admin, I want to manage products in the system by creating, reading, updating, and deleting product records.

Acceptance Criteria:

- Define a Product entity with fields such as name, description, price, and stock quantity.
- Implement API endpoints to perform CRUD (Create, Read, Update, Delete) operations on products.
- Include unit tests (UT) to verify the correctness of CRUD operations for products.

### User Story 4: Add Authentication to the API

Description: As a developer, I want to add authentication to the API to secure access to protected endpoints.

Acceptance Criteria:

- Implement authentication mechanisms, such as JWT (JSON Web Tokens) or OAuth, to secure API endpoints.
- Ensure that only authenticated users have access to certain endpoints, such as product management.
- Document the authentication process and include it in the README.md for reference.