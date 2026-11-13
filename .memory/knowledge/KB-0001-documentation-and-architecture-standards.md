---
id: KB-0001
title: Documentation and Architecture Governance Standards
component: all
domain: governance
tags:
- ste100
- clean-architecture
- standards
status: validated
created: '2026-03-31'
updated: '2026-03-31'
---

# KB-0001: Documentation and Architecture Governance Standards

## Summary
The Hippo Agent Platform mandates two governance standards across code and documentation:
1. **ASD-STE100 (Simplified Technical English)**: All specifications, memory records, and architectural documents must follow ASD-STE100 rules for clarity and consistency.
2. **Clean Architecture Boundary Rules**: Domain entities must never reference application, infrastructure, or web presentation assemblies.

## Applicability
Applies to all software engineers, documentation authors, and automated AI agents contributing to this repository.

## Key Points
- Keep sentences concise (under 25 words where feasible).
- Use active voice and unambiguous terminology.
- Isolate domain business logic completely from database ORMs and external APIs.
- Keep dependency direction strictly pointing inward: Presentation -> Application -> Domain <- Infrastructure.