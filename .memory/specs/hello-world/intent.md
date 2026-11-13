---
title: "Intent: Hello World Endpoint"
created: 2026-08-07
updated: 2026-08-07
depends-on: 
  - ".memory/specs/hello-world/proposal.md"
supported-by: 
  - ".memory/specs/hello-world/tasks.md"
---

# Intent: Hello World Endpoint

## Objective
Provide a secure and simple baseline HTTP endpoint for Delivery Engineers to test API connectivity and JWT token authentication against the Hippo Agent Platform. 

## Boundaries & Scope Constraints
- **Untouchable:** No database schema changes are permitted.
- **Scope Limits:** Only implement the API project and the specific hello-world route. Do not build the web administration portal or any complex domain workflows.

## Technical Constraints
- **Dependencies:** Must use .NET 9 ASP.NET Core Minimal APIs or Controllers.
- **Performance:** API response must be < 200ms.
- **Compatibility:** Must conform to standard JSON responses and RFC 7807 for error handling.

## Data Definitions & Schemas

### Code Data Structures
- **DTOs (Data Transfer Objects):** 
  - `HelloWorldResponse`: A C# record representing the JSON greeting.

## API & Integration Contracts

### Endpoints (REST / RPC / GraphQL)
- **Endpoint Definitions:**
  | Method | Path / Operation | Description | Changes data | Auth Required |
  | :--- | :--- | :--- | :--- | :--- |
  | `GET` | `/api/v1/hello-world` | List greeting | No | Yes |

### Payload Contracts
- **Response Payloads:** Successful 200 OK response.
  ```json
  {
    "message": "Hello, World!"
  }
  ```
- **Error Responses:** 401 Unauthorized for missing/invalid JWT tokens.

## Validation Rules

### Test Sets (Input/Output Rulesets)
- Given a valid JWT token in the Authorization header, the system must return a 200 response with `{"message": "Hello, World!"}`.
- Given no Authorization header, the system must return a 401 response.

## Measurable Success Criteria
- The `/api/v1/hello-world` endpoint returns a valid JSON response containing a greeting message.
- The endpoint responds to valid requests in less than 200 milliseconds.
- The endpoint rejects unauthenticated requests with an HTTP 401 Unauthorized status.

## Implementation Directives
- Bootstrap the `Hippo.Agents.Presentation.Api` project if it does not yet exist.
- Ensure integration tests use `WebApplicationFactory` to verify endpoints.