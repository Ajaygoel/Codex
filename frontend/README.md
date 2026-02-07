# Frontend module

## Purpose

The frontend module is an Angular single-page application that lets staff schedule appointments, review upcoming visits, and manage tenant/user context headers for API requests.

## Entry points

- `src/main.ts`: Angular bootstrap entry point.
- `src/app/app.component.ts`: Primary UI shell that hosts booking, dashboard, and admin views.
- `src/app/services/appointment.service.ts`: API client for appointment and tenant data.
- `src/environments`: Environment configuration for API base URLs.
