# ScheduleIT - DDD and Clean Architecture with .NET 8 (ASP .NET Core)

## Overview

Welcome to ScheduleIT, a project that showcases the implementation of Domain-Driven Design (DDD) and Clean Architecture principles using .NET 8 (ASP .NET Core). The application is containerized using Docker Containers and orchestrated with Docker Compose for simplified deployment.

## Domain Overview

The domain of ScheduleIT revolves around several aggregate roots:

- **Employee:** Represents individuals within the system.
  
- **Team:** Groups employees with different roles such as Team Leader, Software Engineer, Tester, etc.
  
- **Project:** Represents a project assigned to a team with tasks created by team members.

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

For `domain events`, the `outbox pattern` is utilized, persisting events in a database. `Integration events`, meant for external systems, are handled using `RabbitMQ` as the message broker.

## Getting Started

To run the ScheduleIT project locally, you need `Docker`.
Please, follow these steps:

   ```bash
   git clone https://github.com/Shaveek23/ScheduleIT.git
   cd ScheduleIT
   docker-compose up --build
  ```
or...

Open Project in `Visual Studio`, set `'docker-compose'` as a startup project and run.
