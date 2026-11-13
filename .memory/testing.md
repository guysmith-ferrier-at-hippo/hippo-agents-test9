# Hippo Agent Platform — Test Strategy

## Overview

This test strategy defines the quality standards, verification levels, and automated tooling for the Hippo Agent Platform. The strategy guarantees that domain business rules, API integrations, web dashboard accessibility, performance targets, and security baselines undergo automated verification before deployment.

## Test Environments & Access

- **Environments:**
  - **Local Development**: Developer workstations running local .NET 9 SDK, Docker, and Testcontainers.
  - **CI Runner**: Ephemeral GitHub Actions Linux runners running build, static analysis, unit, and integration tests.
  - **Staging / QA**: Containerized cloud environment replicating production configuration for end-to-end and performance verification.
  - **Production**: Live containerized deployment environment with synthetic health monitoring and active telemetry.
- **Access Needs:**
  - Database access credentials managed through secure environment variables and secrets vaults.
  - API keys and tokens for test agent mock services.
- **Data Management:**
  - Test suites use isolated database schemas seeded per test fixture and wiped after run completion.
  - No production customer data or live credentials are used in automated test environments.
- **CI/CD Integration:**
  - Every pull request triggers build compilation, code style formatting, security scanning, unit tests, and containerized integration tests.
  - Merges to `main` trigger deployment to staging and execution of automated end-to-end regression suites.

## Testing Approach

| Level | Scope / Objective | Tools / Frameworks | Success Criteria | Responsibilities |
| :--- | :--- | :--- | :--- | :--- |
| **Unit** | Verify domain logic, entities, value objects, and application command/query handlers in isolation. | xUnit, FluentAssertions, NSubstitute | >= 80% code coverage across Domain and Application layers; 100% pass rate. | Software Engineers |
| **Component (Web)** | Verify Razor / Blazor components, forms, and UI states in isolation. | bUnit, Playwright Component Testing | UI components render correctly across valid and invalid state bounds. | Software Engineers |
| **Contract** | Verify API schemas and request/response contracts against consumer expectations. | Pact.Net | No breaking schema changes detected without version deprecation. | Software Engineers, QA |
| **Integration (Mocked)** | Verify application layer handlers with mocked external dependencies and services. | xUnit, NSubstitute | Handlers gracefully process simulated external failures and edge conditions. | Software Engineers |
| **Integration (API / Events)** | Verify API endpoints, middleware, and EF Core data mapping against real PostgreSQL instances. | WebApplicationFactory, Testcontainers.NET | 100% pass rate on integration test suites with zero database leakage. | Software Engineers, QA |
| **Acceptance (Functional)** | Verify business workflow scenarios and end-to-end user stories against acceptance criteria. | SpecFlow / Reqnroll, xUnit | All defined user story acceptance scenarios pass. | QA, Software Engineers |
| **End-to-End (E2E)** | Verify complete user journeys in browser across the administrative dashboard. | Playwright | Critical user journeys execute successfully without flakiness across desktop and tablet viewports. | QA, Software Engineers |
| **Performance (API)** | Measure throughput, error rates, and response latency under normal and peak loads. | k6 | p95 response latency < 200ms at 1,000 requests per second; 0% unhandled errors. | QA, DevOps, Software Engineers |
| **Performance (Web)** | Measure browser load speed, rendering performance, and Core Web Vitals. | Google Lighthouse, Playwright Tracing | Largest Contentful Paint (LCP) < 2.5s; Total Blocking Time (TBT) < 200ms; Performance score >= 90. | Frontend Engineers, QA |
| **Security** | Static code analysis (SAST), software composition analysis (SCA), and dynamic scans. | SonarQube, GitHub Dependency Review, OWASP ZAP | Zero Critical or High severity vulnerabilities in code or dependencies. | Security Lead, Software Engineers |
| **Accessibility** | Verify WCAG 2.2 AA compliance, keyboard navigation, and screen reader labels. | axe-core, Playwright Axe, manual keyboard audits | 100% compliance with WCAG 2.2 Level AA; zero automated accessibility rule violations. | QA, UX Designer, Software Engineers |

## Risk & Mitigation

| Risk | Mitigation |
| :--- | :--- |
| **E2E test flakiness from asynchronous operations** | Use state-based expectations (for example `WaitForSelectorAsync` or Playwright auto-waiting assertions). Never use hardcoded time delays (`Thread.Sleep`). |
| **Test data contamination across parallel runs** | Execute integration tests in isolated database instances managed dynamically through Testcontainers. |
| **Third-party agent API latency or outages** | Encapsulate third-party agent interactions behind adapters and provide resilient mock implementations for testing. |
| **Docker daemon access in CI runners** | Validate CI environment runner specifications early ([ASSUME-0003](assumptions/ASSUME-0003-ci-docker-testcontainers.md)). |

## Reviewed by

| Name | Role | Approved | Date |
| :--- | :--- | :--- | :--- |
| Project Lead | Test Lead | [x] | 2026-03-31 |
| Tech Lead | Technical Architect | [x] | 2026-03-31 |
| Product Owner | Product Manager | [x] | 2026-03-31 |
