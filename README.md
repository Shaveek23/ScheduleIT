# ScheduleIT - DDD and Clean Architecture with .NET 8 (ASP .NET Core)

## Overview

Welcome to ScheduleIT, a project that showcases the implementation of Domain-Driven Design (DDD) and Clean Architecture principles using .NET 8 (ASP .NET Core). The application is containerized using Docker Containers and orchestrated with Docker Compose for simplified deployment.

## Domain Overview

The domain of ScheduleIT revolves around several aggregate roots:

- **Employee:** Represents individuals within the system.
  
- **Team:** Groups employees with different roles such as Team Leader, Software Engineer, Tester, etc.
  
- **Project:** Represents a project assigned to a team with tasks created by team members.

![image](https://github.com/Shaveek23/ScheduleIT/assets/29927221/ecf8c73b-3562-487a-9989-8b0c8f0172ab)



## Use Cases

The project implements various use cases, including:

- **Registering a new employee:** Adding a new person to the system.

- **Logging in with JWT authorization:** Providing secure access to authenticated users.

- **Creating a new team:** Establishing teams with defined roles.

Please note that while these use cases are implemented, the **project is designed for showcasing purposes** and is not intended for real-world use. It serves as a reference for implementing DDD, Clean Architecture, and associated technologies.

## Architecture and Technologies

### CQRS Pattern with MediatR

The project utilizes the Command Query Responsibility Segregation (`CQRS`) pattern implemented with the `MediatR` library. This enhances modularity by separating command and query operations.

### Entity Framework Core

`Entity Framework Core` is the chosen Object-Relational Mapping (ORM) tool, providing a robust and efficient data access layer.

### FluentValidation

`FluentValidation` is employed for validating command and query DTOs, ensuring the integrity of incoming data.

### FluentAssertion and xUnit

Unit tests are written using `xUnit`, with `FluentAssertions` for clear and expressive assertions, ensuring the reliability of the codebase.

### Event Handling

For `domain events`, the `outbox pattern` is utilized, persisting events in a database and then publishing them via MediatR with a background `Quartz` job. `Integration events`, meant for external systems, are handled using `RabbitMQ` as the message broker.

## Getting Started

To run the ScheduleIT project locally, you need `Docker`.
(or... `Open Project` in `Visual Studio`, set `'docker-compose'` as a startup project and run.
You will see the `SwaggerUI` page to test the API)

If you want to start the application without `Visual Studio` please follow these steps:
   ```bash
   git clone https://github.com/Shaveek23/ScheduleIT.git
   cd ScheduleIT
   docker-compose up --build
  ```
If you decide to rerun the app and want to start with a fresh database, you can remove the data volumes using the following command:
   ```bash
   docker-compose down --volumes
   docker-compose up --build
  ```

Now open, e.g., `Postman` and `register` a new user and obtain an authentication token to include in the headers of subsequent requests.

![image](https://github.com/Shaveek23/ScheduleIT/assets/29927221/be08e5ee-6ca2-4ae4-9433-76669670fc06)


You can explore by providing an invalid email or password to verify that the validation is working.

After successfully registering a new employee, experiment with creating new teams.
Please remember to include the token.
![image](https://github.com/Shaveek23/ScheduleIT/assets/29927221/7328ad74-5805-44dd-8b81-c9eeabc0f9d1)

![image](https://github.com/Shaveek23/ScheduleIT/assets/29927221/f8565bc9-1eda-40a9-b67a-057d4ecdcc1e)


Try creating another team to see that a given employee can create only one team.

If you want to, you can examine the database and the `RabbitMQ` to see that the integration event has been published.
To do that, you need to access the terminal of the specific Docker containers.
  
