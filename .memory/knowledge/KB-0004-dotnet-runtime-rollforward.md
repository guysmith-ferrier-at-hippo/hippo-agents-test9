---
id: KB-0004
title: .NET runtime roll forward configuration
component: build-infrastructure
domain: infrastructure
tags:
- dotnet
- runtime
- rollforward
status: validated
created: '2026-08-07'
updated: '2026-08-07'
depends_on:
- .memory/architecture.md
supported_by: []
---

## What Happened

The integration test run failed because the system did not find the .NET 9 runtime host. Newer .NET runtimes were present on the machine, but the host prevented execution.

## Key points

- Add `<RollForward>LatestMajor</RollForward>` to project files.
- This setting permits the .NET host to execute assemblies on newer runtime versions.
- The project still compiles against the specified target framework.

## Why It Matters

Different environments install different .NET runtime patches or major versions. Rigid runtime matching causes build and test failures on developer machines and CI agents.

## Applies When

Applies when projects compile against a specific .NET target framework but must execute across diverse host environments.
