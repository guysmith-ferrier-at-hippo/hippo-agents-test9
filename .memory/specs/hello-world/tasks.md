---
title: "Hello World Endpoint Tasks"
component: "API"
domain: "testing"
tags: ["hello-world", "api", "connectivity"]
created: 2026-08-07
updated: 2026-08-07
depends-on: 
  - ".memory/specs/hello-world/intent.md"
supported-by: 
  - ".memory/specs/hello-world/proposal.md"
---

## 1. Project Initialization

- [ ] 1.1 Create the `Hippo.Agents.Presentation.Api` ASP.NET Core Web API project.
- [ ] 1.2 Create the `Hippo.Agents.IntegrationTests` xUnit test project.
- [ ] 1.3 Add standard JWT Authentication middleware to the API project program configuration.

## 2. Implement Hello World Route

- [ ] 2.1 Create the `HelloWorldResponse` DTO record containing a `Message` string property.
- [ ] 2.2 Create the `GET /api/v1/hello-world` Minimal API endpoint or Controller.
- [ ] 2.3 Configure the endpoint to return the `HelloWorldResponse` with value "Hello, World!".
- [ ] 2.4 Apply the `[Authorize]` attribute to the hello-world endpoint to require a JWT.

## 3. Verify Implementation

- [ ] 3.1 Write integration tests to implement `TC-01` (Authenticated Request Returns Greeting).
- [ ] 3.2 Write integration tests to implement `TC-02` (Unauthenticated Request is Rejected).
- [ ] 3.3 Write integration tests to implement `TC-03` (Invalid Authentication Token is Rejected).
- [ ] 3.4 Write integration tests to implement `TC-04` (Unsupported HTTP Method).
- [ ] 3.5 Execute tests to ensure passing conditions and verify latency is under 200ms.