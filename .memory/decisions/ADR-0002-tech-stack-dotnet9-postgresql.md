---
id: ADR-0002
title: Technology Stack Selection (.NET 9 and PostgreSQL)
component: backend
domain: technology-stack
tags:
- dotnet
- csharp
- postgresql
- efcore
status: accepted
created: '2026-03-31'
updated: '2026-03-31'
---

# ADR-0002: Technology Stack Selection (.NET 9 and PostgreSQL)

## Context
The Hippo Agent Platform requires a high-performance, strongly typed runtime for orchestrating agent workloads, managing concurrent API requests, and storing workflow state and audit logs reliably.

## Options Considered
1. **C# / .NET 9 with PostgreSQL & EF Core**: Modern high-performance runtime, cross-platform container support, robust ecosystem for enterprise security and testing.
2. **Node.js / TypeScript with PostgreSQL**: High concurrency, but less rigid type safety across large enterprise domains and heavier maintenance of manual DI patterns.
3. **Python with FastAPI & PostgreSQL**: Good for ad-hoc agent scripts, but lower execution performance and less comprehensive enterprise testing infrastructure for multi-layer solutions.

## Decision
We select **C# on .NET 9 (ASP.NET Core)** for the backend services and **PostgreSQL** (via Entity Framework Core) as the relational database.

## Consequences
- **Positive**: First-class asynchronous programming, strong typing, built-in dependency injection, and high throughput.
- **Positive**: Seamless containerization and comprehensive integration testing via Testcontainers.
- **Negative**: Requires team familiarity with modern C# and EF Core migration workflows.