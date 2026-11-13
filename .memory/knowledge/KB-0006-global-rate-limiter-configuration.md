---
id: KB-0006
title: Global rate limiter configuration in ASP.NET Core
component: API/rate-limiting
domain: security
tags:
- rate-limiting
- security
- aspnetcore
- dos-prevention
status: validated
created: '2026-08-07'
updated: '2026-08-07'
depends_on:
- .memory/reports/code-review-report.md
supported_by: []
---

## What Happened

The initial setup added rate limiting middleware without a global partitioner. The review flagged a high denial of service risk because request counts remained unrestricted. A global fixed-window rate limiter was added to restrict request volumes.

## Key points

- Configure `options.GlobalLimiter` when adding rate limiting middleware.
- Set `options.RejectionStatusCode` to `StatusCodes.Status429TooManyRequests`.
- Test rate limits with an isolated `WebApplicationFactory` instance.

## Why It Matters

Without a global limiter, endpoints remain vulnerable to volumetric abuse and resource exhaustion. Setting a default global policy ensures protection for all routes.

## Applies When

Applies when you configure ASP.NET Core rate limiting middleware to protect public APIs against denial of service attacks.