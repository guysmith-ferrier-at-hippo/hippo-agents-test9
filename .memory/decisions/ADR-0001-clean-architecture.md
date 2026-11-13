---
id: ADR-0001
title: Adoption of Clean Architecture Pattern
component: core-architecture
domain: architecture
tags:
- clean-architecture
- dotnet
- layers
status: accepted
created: '2026-03-31'
updated: '2026-03-31'
---

# ADR-0001: Adoption of Clean Architecture Pattern

## Context
The Hippo Agent Platform must support maintainability, clear separation of concerns, and high testability across business logic, agent orchestration, persistence, and external presentation interfaces. We need an architectural structure that isolates domain logic from infrastructure dependencies and external frameworks.

## Options Considered
1. **Clean Architecture (Onion / Hexagonal)**: Clear isolation of Domain, Application, Infrastructure, and Presentation layers.
2. **Traditional 3-Tier Layered Architecture**: Presentation, Business Logic, and Data Access tightly coupled to data schemas.
3. **Flat Minimal API Monolith**: Fast prototyping but lower long-term maintainability for complex orchestration workflows.

## Decision
We will use Clean Architecture with four distinct layers:
- **Domain**: Enterprise entities, value objects, domain logic, and interfaces. No external framework dependencies.
- **Application**: Use cases, workflow orchestrations, command and query handlers, DTOs, and application interfaces.
- **Infrastructure**: Database persistence (EF Core, PostgreSQL), external API clients, logging, and identity integrations.
- **Presentation / API**: ASP.NET Core Web API controllers / minimal endpoints, middleware, and web UI.

## Consequences
- **Positive**: High testability; domain logic remains independent of database and UI technology choices.
- **Positive**: Strict inward dependency flow prevents business rules from coupling to infrastructure.
- **Negative**: Initial overhead of project boilerplate and mapping across layer boundaries.