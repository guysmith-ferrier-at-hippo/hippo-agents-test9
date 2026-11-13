---
id: DD-0001
title: Adoption of GDS Design System for Administrative UI
component: frontend-ui
domain: design-system
tags:
- gds
- accessibility
- ui
- design-system
status: accepted
created: '2026-03-31'
updated: '2026-03-31'
---

# DD-0001: Adoption of GDS Design System for Administrative UI

## Context
The Hippo Agent Platform provides an administrative web dashboard for configuring agents, inspecting workflows, and monitoring system telemetry. We require a dependable, accessible, and consistent component system that minimizes custom styling overhead and prioritizes usability.

## Options Considered
1. **GDS (GOV.UK Design System)**: Highly accessible (WCAG 2.2 AA compliant), thoroughly researched, authoritative, and clean user experience.
2. **Tailwind UI / Custom Components**: Highly flexible, but requires significant bespoke styling and continuous accessibility verification.
3. **Bootstrap 5**: Readily available, but lacks the rigorous accessibility standards and clean cognitive load of GDS.

## Decision
We adopt the **GDS Design System** tokens, typographic scale, colour palette, and component patterns for all administrative dashboard views.

## Consequences
- **Positive**: Out-of-the-box WCAG 2.2 AA compliance, consistent navigation patterns, and predictable layout across desktop and tablet devices.
- **Positive**: Low cognitive load for administrators inspecting complex workflow data.
- **Negative**: Certain complex custom data-grid components must be carefully adapted to comply with GDS styling constraints.