This project is a .NET 8 backend service for a task management system, designed with user authentication in mind. It's built to allow users to securely create, view, update, 
and delete tasks associated with their account. The architecture choices prioritize maintainability, testability, and scalability.

This project follows a Clean Architecture approach, which provides a clear separation of concerns by organizing code into distinct, concentric layers. This design ensures that 
the core business logic remains independent of external frameworks, databases, and UI layers. The dependency rule is key: inner layers have no knowledge of outer layers.
- API (Presentation Layer): This is the outermost layer and the entry point for the application. It handles HTTP requests, authentication, and error handling. It leverages MediatR
  to dispatch incoming requests to the Domain layer and uses AutoMapper to translate Data Transfer Objects (DTOs) from the API into domain commands and models. This decoupling ensures
  the API layer is "thin" and focused solely on external communication.
- Domain (Core Layer): This is the heart of the application, containing the business logic, entities, and interfaces. It is completely independent of other layers, meaning it has no
  references to the database or web framework. We use the Command/Query Responsibility Segregation (CQRS) pattern with MediatR to separate operations that change data (Commands) from
  those that retrieve it (Queries). This approach provides better performance, scalability, and clarity.
- Storage (Infrastructure Layer): This layer is responsible for data persistence. It contains the concrete implementations of the interfaces defined in the Domain layer. We use Entity
  Framework Core with PostgreSQL to interact with the database. This layer also houses data entities, migrations, and the DbContext, ensuring that data access logic is isolated and can
  be swapped out without affecting the business logic.

Getting this project up and running is straightforward with Docker.
Prerequisites
- Docker installed and running on your system.
- .NET 8 SDK if you plan to make code changes or run migrations manually.
Steps:
1. Clone the repository and navigate to the project directory.
2. Ensure your docker-compose.yml file is configured with the correct database and port settings. You can modify these as needed.
3. Use Docker Compose to build and start the application and database containers with a single command: docker-compose up --build
   This command will build the Docker images, run the containers, and automatically apply database migrations and seed initial data, thanks to the configuration in the Program.cs file.
4. The backend API will be available at http://localhost:7211. You can access the Swagger UI at http://localhost:7211/swagger to explore and test the available API endpoints.

API Documentation
Endpoints:

o	POST /api/Account/registration:
Request body:
{
  “username”: “username1”,
  “email”: “user@example.com”,
  “password”: “password_123”
}
Response: 200 OK if registration is successful.

o	POST /api/Account/login: To authenticate a user and return a JWT.
Request body:
{
  "email": "string",
  "password": "string"
}
Response: 200 OK with token.

o	POST /api/Task/create-task: To create a new task (authenticated).
Request body:
{
  "title": "string",
  "description": "string",
  "dueDate": "2025-08-31T15:10:01.083Z",
  "status": 0,
  "priority": 0
}

o	GET /api/Task/get-all-tasks: To retrieve a list of tasks for the authenticated user.
Query Parameters:
Page: (int)
ItemPerPage: (int)
Response: 200 OK with a paginated list of tasks.

o	GET /api/Task/get-task/{id}: To retrieve the details of a specific task by its ID (authenticated).

o	PUT /api/Task/update-task: To update an existing task (authenticated).
Request body:
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "description": "string",
  "dueDate": "2025-08-31T15:10:01.087Z",
  "status": 0,
  "priority": 0
}

o	DELETE /api/Task/delete-task/{id}: To delete a specific task by its ID (authenticated).
