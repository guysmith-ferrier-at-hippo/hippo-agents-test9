---
id: ASSUME-0003
title: Docker daemon availability in CI runners for Testcontainers
component: ci-cd
domain: testing
tags:
- ci-cd
- docker
- testcontainers
- testing
status: unresolved
created: '2026-03-31'
updated: '2026-03-31'
---

## 1. Assumption Description
The CI/CD pipeline environment (GitHub Actions Linux runners) supports running Docker containers to execute automated integration tests with Testcontainers.NET.

## 2. Context & Rationale
The integration test suite spins up real PostgreSQL instances using Testcontainers. This requires Docker daemon access within the test runner agent.

## 3. Impact If Incorrect
If Docker daemon access is restricted in the runner environment, integration tests will require dedicated hosted test database instances or in-memory emulation mocks.

## 4. Verification Path
Validate GitHub Actions runner capabilities and run a containerized test job in the initial pipeline verification step.