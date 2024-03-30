# Blazor demo project

This a demo project meant to showcase various best practices and technologies for .NET and Blazor.

## Features

**Backend**

- Layered architecture with separation of concerns using multiple projects.
- Repository pattern.
- Optionally Unit of Work pattern.
- Code first approach with Entity Framework Core and migrations.
- Entities, DTOs, and view models.
- Object mapping.
- Data validation.
- API error handling.
- Logging.

**Frontend**

- Different component libraries:
  - MudBlazor
  - FluentUI
- Tree structures.
- Forms.
- Generic components using render fragments.
- Error handling.
- Tailwind.

**Testing**

- Database integration tests.
- API integration tests.
- Mocking.
- Acceptance tests.
- Frontend tests.
- Benchmarking if I find something to benchmark.

## Repository pattern

I have many complicated feelings about it, but this is an exploration of how to implement and use it.

Not everything might be optimal, and this might be mixing layers, but I also want to show examples of how AutoMapper integrates with the ORM.
