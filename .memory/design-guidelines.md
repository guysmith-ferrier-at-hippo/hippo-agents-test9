# Hippo Agent Platform — Design Guidelines

> This document is the canonical design reference for the project. Every contributor and AI agent skill reads this document before generating or reviewing UI. When in doubt, this file is the tiebreaker.
>
> All rules in this document are sourced directly from the GDS Design System (https://design-system.service.gov.uk) and the GOV.UK Component Guide (https://components.publishing.service.gov.uk/component-guide).

---

## 1. Design Philosophy

The Hippo Agent Platform administrative portal prioritizes clarity, task completion, and low cognitive load. Users inspect complex workflow telemetry, configure automated agents, and monitor platform health.

The restraint of the GDS design language communicates authority, calm, and dependability. Every design decision must serve that goal. Do not use decorative visual complexity when clear content solves the user need.

---

## 2. Design System Reference

All user interfaces must use components, tokens, and patterns from these systems in order of priority:

1. **GDS Design System** — https://design-system.service.gov.uk
2. **GOV.UK Component Guide** — https://components.publishing.service.gov.uk/component-guide
3. **Internal Hippo Design Decisions** — Referenced in `.memory/decisions/` (such as [DD-0001](decisions/DD-0001-gds-design-system.md)).

---

## 3. Colour

*Source: https://design-system.service.gov.uk/styles/colour/*

All colours must use GDS design tokens. Never write hardcoded hex values in component stylesheets.

### Functional Colour Tokens

| Token | Hex | Use |
| :--- | :--- | :--- |
| `text` | `#0b0c0c` | All body text |
| `secondary-text` | `#484949` | Supporting and metadata text |
| `body-background` | `#ffffff` | Primary page background |
| `link` | `#1a65a6` | Default interactive link colour |
| `link-hover` | `#0f385c` | Link hover state |
| `link-visited` | `#54319f` | Visited link state |
| `focus` | `#ffdd00` | Mandatory focus ring only |
| `focus-text` | `#0b0c0c` | Text colour on focus background |
| `error` | `#d4351c` | Error messages and failed states |
| `success` | `#00703c` | Success indicators and completed tasks |
| `border` | `#b1b4b6` | Component borders and horizontal dividers |

### Colour Rules

- Always apply the standard GDS colour palette first.
- Never use colour as the single indicator of system status. Always include explanatory text or iconography.
- The focus ring colour (`#ffdd00`) is mandatory for WCAG 2.2 compliance. Never override or remove it.

---

## 4. Typography

*Source: https://design-system.service.gov.uk/styles/headings/*

### Heading Classes

| Class | HTML Tag | Use |
| :--- | :--- | :--- |
| `govuk-heading-xl` | `h1` | Primary page title. Exactly one per page. |
| `govuk-heading-l` | `h2` | Major sections inside a page. |
| `govuk-heading-m` | `h3` | Sub-sections inside an `h2` block. |
| `govuk-heading-s` | `h4` | Fine-grained group headers. |
| `govuk-caption-xl` | `span` inside `h1` | Section category above the page title. |

### Body Text Classes

| Class | Tag | Use |
| :--- | :--- | :--- |
| `govuk-body-l` | `p` | Lead paragraph immediately after `h1`. |
| `govuk-body` | `p` | Standard body copy and operational descriptions. |
| `govuk-body-s` | `p` | Timestamps, metadata, and footnote text. |

### Typography Rules

- Every heading must include a `govuk-heading-*` CSS class.
- Do not skip heading levels (for example, do not place an `h3` directly inside an `h1`).
- Do not put `<strong>` or `<b>` inside heading elements.
- Headings must use sentence case and contain no trailing full stop.

---

## 5. Layout & Grid

*Source: https://design-system.service.gov.uk/styles/layout/*

### Page Structure Wrappers
- `govuk-width-container`: Enforces maximum content width with responsive margin gutters.
- `govuk-main-wrapper`: Provides consistent vertical padding for main content.

### Column Layouts

| Class | Use |
| :--- | :--- |
| `govuk-grid-column-full` | Wide data tables, metric summaries, and full-width agent timelines. |
| `govuk-grid-column-two-thirds` | Standard forms, configuration views, and body content. |
| `govuk-grid-column-one-third` | Sidebars, summary panels, and contextual links. |
| `govuk-grid-column-one-half` | Balanced two-column comparison panels. |

---

## 6. Navigation

- **Service Header**: Use `govuk-header` with service name "Hippo Agent Platform".
- **Service Navigation**: Use `govuk-service-navigation` for top-level operational sections (Dashboard, Workflows, Agents, Audit Logs, Settings).
- **Breadcrumbs**: Use `govuk-breadcrumbs` for hierarchical pages. Place breadcrumbs inside `govuk-width-container` before `govuk-main-wrapper`.
- **Back Link**: Use `govuk-back-link` on multi-step wizard pages. Do not combine breadcrumbs and back links on the same page.

---

## 7. Page Types

1. **Hub / Dashboard Pages**: Displays high-level status cards, agent health metrics, and summary tables.
2. **Detail / Inspection Pages**: Shows detailed execution logs, payload traces, and historical steps for a selected workflow.
3. **Transactional / Configuration Pages**: Multi-step forms for registering agent credentials, defining workflow parameters, or updating platform settings.

---

## 8. Component Rules

- **Buttons**:
  - `govuk-button`: Primary action (e.g., "Run Workflow", "Save Configuration"). Limit to one primary button per screen context.
  - `govuk-button--secondary`: Supplementary actions (e.g., "Export Logs", "Cancel").
  - `govuk-button--warning`: Irreversible actions (e.g., "Delete Agent Instance").
- **Tables & Summary Lists**: Use `govuk-table` and `govuk-summary-list` for agent metadata and run parameters.
- **Form Controls**: Use `govuk-input`, `govuk-select`, and `govuk-checkboxes` with explicit label association (`for`/`id`).
- **Feedback & Notifications**:
  - `govuk-error-summary`: Rendered at the top of transactional pages when form validation fails.
  - `govuk-notification-banner`: Rendered after successful asynchronous actions or critical updates.

---

## 9. Content & Copy Standards

- **Language Standard**: Follow ASD-STE100 (Simplified Technical English) guidelines.
- **Tone of Voice**: Direct, factual, professional, and clear.
- **Formatting**: Use sentence case for all titles, buttons, and table headers.

---

## 10. Accessibility (a11y)

- **Standard**: WCAG 2.2 Level AA compliance is mandatory.
- **Contrast Ratios**: Minimum 4.5:1 for standard body text; 3:1 for large text and UI boundaries.
- **Keyboard Navigation**: All interactive elements must be accessible via Tab/Shift+Tab and display the yellow `#ffdd00` focus indicator.
- **Screen Readers**: Use semantic HTML landmarks (`<main>`, `<nav>`, `<header>`, `<footer>`) and ARIA roles only where native HTML is insufficient.

---

## 11. Common Anti-patterns to Avoid

- Overriding the default GDS focus outline with `outline: none`.
- Using colour alone to show workflow success or failure.
- Using ambiguous button or link text such as "Click here" or "Submit".
- Creating non-standard bespoke responsive breakpoints.

---

## 12. Design Decisions Requiring Prior Review

Raise a formal design decision record (`save-design-decision`) before:
- Introducing external non-GDS UI components or JavaScript chart libraries.
- Introducing new color variables outside the official GDS palette.
- Changing responsive breakpoints or layout grid wrappers.
