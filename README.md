# Clean Architecture & Domain-Driven Design (DDD) in micro-service using C# .net 8

## Table of Contents
- [Introduction](#introduction)
- [Magic 8 Ball](#magic-8-ball)
- [Architecture Overview](#architecture-overview)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Folder Structure](#folder-structure)
- [Domain Layer](#domain-layer)
- [Application Layer](#application-layer)
- [Infrastructure Layer](#infrastructure-layer)
- [Presentation Layer](#presentation-layer)
- [Testing](#testing)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This repository contains a C# application designed to simulate the functionality of a Magic 8 Ball, using the principles of Clean Architecture and Domain-Driven Design (DDD). The Magic 8 Ball is a toy used for fortune-telling or seeking advice, which provides randomized answers to yes-or-no questions.

## Magic 8 Ball

The Magic 8 Ball is a classic toy invented in the 1950s. It is a black, spherical object resembling an eight-ball from billiards, filled with liquid and containing a 20-sided die. Each face of the die has a different answer, such as "Yes," "No," or "Ask again later." When a user asks a yes-or-no question and shakes the ball, a random answer floats to the top of the window on the ball, providing the user with a whimsical response.

This project takes inspiration from the Magic 8 Ball to create a digital version of this toy, where users can submit questions and receive randomized answers through an API.

## Architecture Overview

The Clean Architecture ensures a clear separation of concerns and enhances maintainability and testability by structuring the application into distinct layers:

1. **Domain Layer**: Contains the core business logic and entities.
2. **Application Layer**: Contains the application logic, service interfaces, and use cases.
3. **Infrastructure Layer**: Contains the implementations of the interfaces defined in the Application Layer, such as data access.
4. **Presentation Layer**: Contains the user interface logic (e.g., Web API controllers).

This project serves as a prototype to demonstrate the implementation of these principles using best practices such as Test-Driven Development (TDD), dependency injection, and separation of concerns.

![Clean Architecture](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

## Technologies Used

- C#
- .NET Core
- Entity Framework Core
- MediatR
- AutoMapper
- FluentValidation
- xUnit

## Getting Started

### Prerequisites

- .NET Core SDK
- SQLLite or any other database of your choice

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/AliAhmadHassan/Magic-8-Ball.git
   cd Magic-8-Ball
   ```

2. Restore the dependencies:
   ```sh
   dotnet restore
   ```

3. Update the connection string in `appsettings.json`.

4. Apply migrations:
   ```sh
   dotnet ef database update
   ```

5. Run the application:
   ```sh
   dotnet run
   ```

## Folder Structure

```
src
|-- Magic8Ball.Domain
|   |-- Entities
|   |-- Interfaces
|   `-- Services
|
|-- Magic8Ball.Application
|   |-- DTOs
|   |-- Interfaces
|   |-- Services
|   |-- UseCases
|   `-- Validators
|
|-- Magic8Ball.Infrastructure
|   |-- Data
|   |-- Repositories
|   `-- Configurations
|
|-- Magic8Ball.Presentation
|   |-- Controllers
|   |-- Models
|   `-- Views
|
|-- tests
|   `-- Magic8Ball.Tests
|       |-- UnitTests
|       `-- IntegrationTests
```

## Domain Layer

This layer contains the core business logic and entities.

### Entities

- **Answer**: Represents a possible response from the Magic 8 Ball, containing properties like `Id`, `Text`, and `Category`.

### Interfaces

- **IAnswerRepository**: Defines the contract for data access operations related to `Answer` entities.

### Services

- **Magic8BallService**: Contains the business logic for providing answers to user questions.

## Application Layer

This layer contains the application logic, service interfaces, and use cases.

### DTOs

- **AnswerDto**: Represents the data transfer object for `Answer`.

### Interfaces

- **IMagic8BallService**: Defines the application service interface for the Magic 8 Ball functionality.

### Services

- **Magic8BallAppService**: Implements `IMagic8BallService`, using the domain services and repositories.

### UseCases

- **GetRandomAnswerUseCase**: Contains the logic for retrieving a random answer from the Magic 8 Ball. This use case is invoked by the controller and encapsulates the business logic.

### Validators

- **QuestionValidator**: Validates the user input using FluentValidation.

## Infrastructure Layer

This layer contains the implementations of the interfaces defined in the Application Layer.

### Data

- **Magic8BallDbContext**: Database context for Entity Framework Core.

### Repositories

- **AnswerRepository**: Implements `IAnswerRepository` for data access operations.

### Configurations

- **DependencyInjectionConfig**: Configures dependency injection for the application.

## Presentation Layer

This layer contains the user interface logic.

### Controllers

- **Magic8BallController**: API controller for handling HTTP requests related to the Magic 8 Ball. The controller invokes use cases to process requests, ensuring the controller remains thin and delegates business logic to the appropriate use case.

### Models

- **QuestionModel**: Represents the user input model.

### Views

- Not applicable for a Web API project.

## Testing

This application follows Test-Driven Development (TDD) principles, ensuring that tests are written before the actual code. The tests are structured to cover different layers of the application.

### Unit Tests

- **UnitTests**: These tests cover individual units of code, such as methods in services and use cases. The unit tests ensure that each part of the application behaves as expected in isolation.

### Integration Tests

- **IntegrationTests**: These tests verify the interactions between different parts of the application, such as the interaction between the application and the database.

To run the tests, use the following command:
```sh
dotnet test
```

## Usage

1. Start the application using `dotnet run`.
2. Access the API endpoint via `http://localhost:5000/api/magic8ball`.
3. Send a POST request with a question to get a randomized answer.

## Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

## Contato
For any questions or feedback, feel free to contact the project maintainer:

Ali Ahmad Hassan

[Perfil no GitHub](https://github.com/AliAhmadHassan)

[Perfil no LinkedIn](https://www.linkedin.com/in/ali-ahmad-hassan/)