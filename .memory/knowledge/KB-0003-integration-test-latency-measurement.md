---
id: KB-0003
title: Integration test response latency measurement
component: testing
domain: testing
tags:
- integration-tests
- latency
- web-application-factory
status: validated
created: '2026-08-07'
updated: '2026-08-07'
depends_on:
- .memory/testing.md
supported_by: []
---

## What Happened

The team added an integration test to measure endpoint response latency. The first HTTP request took more than 400 milliseconds. The test failed because the expected latency was less than 200 milliseconds. The latency was caused by framework startup and JIT compilation.

## Key points

- Send a warmup request before you measure endpoint latency.
- Do not include `WebApplicationFactory` startup time in endpoint performance assertions.
- Measure only the steady-state request time.

## Why It Matters

Startup overhead causes false test failures. Developers waste time investigating slow endpoints when the issue is test initialization.

## Applies When

Applies when writing automated tests with `WebApplicationFactory` that enforce latency or performance thresholds.
