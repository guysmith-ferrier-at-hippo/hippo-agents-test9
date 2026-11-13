# Test Cases: Hello World Endpoint

## 🟢 Happy Path Scenarios

### TC-01: Authenticated Request Returns Greeting
- **Pre-conditions**: A client possesses a valid JWT bearer token.
- **Steps**:
  1. Send an HTTP GET request to `/api/v1/hello-world` with the JWT in the `Authorization` header.
- **Expected Result**: The server returns an HTTP 200 OK status. The response header `Content-Type` is `application/json`. The response body contains `{"message": "Hello, World!"}`. The request completes in under 200ms.

## 🔴 Negative Scenarios

### TC-02: Unauthenticated Request is Rejected
- **Pre-conditions**: A client does not provide any authentication token.
- **Steps**:
  1. Send an HTTP GET request to `/api/v1/hello-world` without an `Authorization` header.
- **Expected Result**: The server returns an HTTP 401 Unauthorized status. The endpoint domain logic is not executed.

### TC-03: Invalid Authentication Token is Rejected
- **Pre-conditions**: A client has an expired or malformed JWT token.
- **Steps**:
  1. Send an HTTP GET request to `/api/v1/hello-world` using the invalid JWT in the `Authorization` header.
- **Expected Result**: The server returns an HTTP 401 Unauthorized status. 

### TC-04: Unsupported HTTP Method
- **Pre-conditions**: A client possesses a valid JWT bearer token.
- **Steps**:
  1. Send an HTTP POST request to `/api/v1/hello-world`.
- **Expected Result**: The server returns an HTTP 405 Method Not Allowed status.

## 🟡 Edge Cases

### TC-05: Performance Under Load
- **Pre-conditions**: The system is running in a staging or production-like environment.
- **Steps**:
  1. Send valid HTTP GET requests at 1,000 requests per second.
- **Expected Result**: The p95 response latency remains under 200ms with zero unhandled errors.