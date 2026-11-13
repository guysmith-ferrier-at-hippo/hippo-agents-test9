---
id: KB-0005
title: Mandatory secret configuration and fail-fast validation
component: API/auth
domain: security
tags:
- security
- jwt
- configuration
- fail-fast
status: validated
created: '2026-08-07'
updated: '2026-08-07'
depends_on:
- .memory/reports/code-review-report.md
supported_by: []
---

## What Happened

The initial implementation used a hardcoded JWT secret key when configuration was missing. A code review flagged this as a critical security risk because attackers could forge tokens. The code was changed to throw an `InvalidOperationException` immediately when configuration is missing.

## Key points

- Do not provide fallback secret values in application code.
- Throw an exception immediately when mandatory security configuration is missing.
- Inject required test configurations explicitly in `WebApplicationFactory`.

## Why It Matters

Hardcoded fallback secrets allow attackers to forge valid authentication tokens if configuration fails. Throwing an exception prevents the application from starting in an insecure state.

## Applies When

Applies when you read secrets, keys, or credentials from `IConfiguration` or environment variables in ASP.NET Core applications.