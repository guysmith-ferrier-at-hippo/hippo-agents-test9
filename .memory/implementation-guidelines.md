# Implementation Guidelines

These guidelines define technical standards and patterns for the Hippo Agent Platform.

## 1. API Route Conventions

- Use lowercase nouns separated by hyphens for route paths.
- Prefix all public endpoints with the API version (for example, `/api/v1/`).
- Use ASP.NET Core Minimal APIs for lightweight and performant endpoints.
- Define explicit response types with `.Produces<T>()` metadata.

## 2. Authentication and Authorization

- Enforce JWT authentication on all external endpoints by default.
- Apply `.RequireAuthorization()` to secure routes.
- Validate token issuer, audience, and signing key in all environments.
- Throw an exception when the JWT secret key configuration is missing.
- Do not use hardcoded fallback secret values.
- Reject unauthenticated requests with HTTP 401 Unauthorized status.

## 3. Data Transfer Objects

- Use C# records for request and response payloads.
- Use PascalCase for record properties. The JSON serializer converts properties to camelCase automatically.
- Do not expose internal database models or private keys in response objects.

## 4. Rate Limiting

- Enable rate limiting middleware in the application pipeline.
- Configure a global rate limiter to protect system resources against excessive requests.
- Return HTTP 429 Too Many Requests when request limits are exceeded.

## 5. Integration Testing

- Use `WebApplicationFactory<Program>` to test HTTP behavior.
- Test endpoints through public HTTP interfaces only.
- Include a warmup request before measuring response latency.
- Set `<RollForward>LatestMajor</RollForward>` in project files to support flexible runtime environments.

## 6. Error Handling

- Register standard problem details services with `AddProblemDetails()`.
- Return RFC 7807 problem details payloads for unhandled errors.
