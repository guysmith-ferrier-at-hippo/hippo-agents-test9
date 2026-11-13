---
id: ASSUME-0001
title: Managed cloud container hosting environment
component: infrastructure
domain: infrastructure
tags:
- cloud
- containers
- hosting
status: unresolved
created: '2026-03-31'
updated: '2026-03-31'
---

## 1. Assumption Description
The Hippo Agent Platform service will deploy to a managed cloud container environment (such as Azure Container Apps or AWS ECS). 

## 2. Context & Rationale
The application architecture uses .NET 9 and Linux Docker containers. Managed container services provide native auto-scaling, container health monitoring, and simplified deployment pipelines without dedicated virtual machine maintenance.

## 3. Impact If Incorrect
If on-premise virtual machines or traditional IIS hosting are required instead, deployment scripts, network security configurations, and container-based CI/CD steps will require re-engineering.

## 4. Verification Path
Confirm hosting infrastructure target with infrastructure and operations stakeholders during the discovery phase.