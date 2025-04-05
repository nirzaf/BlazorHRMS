**Subject: Technical Guidelines - HR Management System (Project HRM)**

**Version:** 1.0
**Date:** 2025-04-05

**1. Introduction**

These guidelines define the architectural principles, technology choices, and core technical approaches for developing the HR Management System (Project HRM). The system aims to provide a robust platform for managing employee data, leave requests, and performance reviews, featuring role-based access control, calendar integration, and document management capabilities. Adherence to these guidelines is crucial for ensuring consistency, maintainability, scalability, and security.

**2. Goals & Requirements Recap**

* **Core Functionality:** Employee Data Management, Leave Request Workflow, Performance Review Process.
* **Key Features:** Role-Based Access Control (RBAC), Calendar Integration, Secure Document Uploads & Management.
* **Target Platform:** Web Application accessible via modern browsers.
* **Technology Stack:** .NET 9, C# 13, Blazor Web App, IdentityServer4 (or recommended alternative), Entity Framework Core 9.

**3. Architectural Principles**

* **Separation of Concerns (SoC):** Strictly partition the application into distinct layers (Presentation, Application/Business Logic, Infrastructure, Domain).
* **Clean Architecture / Onion Architecture:** Adopt one of these patterns to ensure loose coupling, high cohesion, testability, and maintainability. The domain model and business logic should be independent of UI, database, and external services.
* **Domain-Driven Design (DDD):** Apply DDD principles where appropriate, focusing on the core domain (Employee, Leave, Performance) and ubiquitous language. Define aggregates, entities, value objects, and domain events.
* **Modularity:** Structure the application into logical modules (e.g., Employee Module, Leave Module, Performance Module, Security Module, Shared Kernel). Consider a Modular Monolith approach initially for balanced complexity and deployment simplicity.
* **Security by Design:** Integrate security considerations into every stage of design and development.
* **Scalability & Performance:** Design components with future scaling in mind, even if starting monolithic. Optimize data access and leverage asynchronous operations extensively.

**4. Technology Stack Specification**

* **Core Framework:** .NET 9 SDK.
* **Language:** C# 13 (Leverage latest language features like improved pattern matching, collection initializers etc., where applicable for clarity and conciseness).
* **Web Framework:** Blazor Web App (.NET 9).
    * **Render Mode:** Primarily utilize **Interactive Server** mode for rich interactivity without complex client-side state management initially. Consider **Auto** mode if specific components benefit significantly from WebAssembly (WASM) for performance or offline capabilities, allowing progressive enhancement. Avoid defaulting to global WASM unless explicitly justified due to initial load time and complexity.
* **Authentication & Authorization:**
    * **Identity Provider:** IdentityServer4 (as requested).
        * **IMPORTANT CAVEAT:** IdentityServer4 is no longer actively maintained by the original creators for open-source use beyond November 2022 security updates. The recommended successor is **Duende IdentityServer**, which requires commercial licensing for production use beyond certain thresholds. **Strongly recommend evaluating Duende IdentityServer or utilizing the built-in OIDC/OAuth features within ASP.NET Core Identity directly, possibly integrating with cloud providers like Microsoft Entra ID (Azure AD) if feasible.** If sticking with IS4, acknowledge the support limitations and potential security risks.
    * **User/Role Store:** ASP.NET Core Identity integrated with Entity Framework Core 9.
    * **Protocol:** OpenID Connect (OIDC) for authentication, OAuth 2.0 for API authorization (if APIs are exposed separately).
* **Database:**
    * **Type:** Relational Database (e.g., PostgreSQL 16+, SQL Server 2022+). Choice depends on hosting environment and existing infrastructure/expertise. PostgreSQL is often preferred for new projects due to its features and licensing.
    * **ORM:** Entity Framework Core 9. Utilize DbContext pooling, compiled models (if performance dictates), and efficient querying techniques.
* **UI Components:** Utilize a mature Blazor component library (e.g., MudBlazor, Radzen Blazor Components, Telerik UI for Blazor) for consistent UI/UX and accelerated development.
* **Validation:** FluentValidation library for robust, decoupled validation logic.
* **Background Jobs:** Hangfire or Quartz.NET for scheduling tasks (e.g., leave balance accruals, notification reminders, report generation).
* **Logging:** Serilog or NLog, configured for structured logging. Integrate with a centralized logging platform (e.g., Seq, ELK Stack, Azure Monitor).
* **Caching:** Implement caching strategies (in-memory `IMemoryCache`, distributed `IDistributedCache` with Redis) for frequently accessed, relatively static data (e.g., configuration, user roles/permissions, dropdown lists).

**5. System Design & Implementation Guidelines**

* **Solution Structure:** Organize the solution based on the chosen architectural pattern (e.g., `Domain`, `Application`, `Infrastructure`, `WebUI` projects/folders). Implement modularity within these layers.
* **Authentication Flow:**
    * Blazor Web App acts as an OIDC client.
    * IdentityServer4 (or alternative) handles user login, consent, and issues identity/access tokens.
    * Use ASP.NET Core Identity for managing user credentials, profiles, and roles within the Identity Provider.
* **Authorization (RBAC):**
    * Define application roles (e.g., `Employee`, `Manager`, `HRAdmin`).
    * Assign users to roles via ASP.NET Core Identity.
    * Implement authorization using `[Authorize]` attributes with roles and/or policy-based authorization for fine-grained control over features and data access. Policies should be defined centrally.
* **Data Modeling (EF Core):**
    * Design entities following DDD principles where applicable (rich domain models preferred over anemic ones).
    * Use EF Core Migrations for database schema evolution.
    * Configure relationships, constraints, indexes, and value conversions appropriately using Fluent API.
    * Implement soft deletes (`IsDeleted` flag) for critical data where necessary.
    * Pay attention to PII data; identify and plan for potential encryption at rest if required.
* **API Design (Internal/if needed):** If internal APIs are developed (e.g., for future mobile clients or microservices), adhere to RESTful principles or consider gRPC for high-performance inter-service communication. Use OpenAPI/Swagger for documentation.
* **Business Logic:** Encapsulate core business rules within Application Services or Domain Services. Use patterns like MediatR (for CQRS) to decouple command/query handling if complexity warrants it.
* **Employee Data Module:**
    * Core entity: `Employee` (personal details, contact info, job details, employment history).
    * Consider data privacy implications (GDPR, etc.).
    * Implement audit trails for changes to sensitive employee data.
* **Leave Request Module:**
    * Entities: `LeaveRequest`, `LeaveType`, `LeaveBalance`.
    * Workflow: Define states (Submitted, Approved, Rejected, Cancelled) and transitions. Implement state management logic.
    * Logic for balance calculation, accrual (potentially via background jobs).
* **Performance Review Module:**
    * Entities: `PerformanceReview`, `ReviewTemplate`, `ReviewSection`, `Rating`, `Feedback`.
    * Support configurable review cycles and templates.
* **Document Uploads:**
    * **Storage:** Utilize cloud blob storage (Azure Blob Storage, AWS S3) for scalability, durability, and security. Avoid storing files directly in the database or on the web server's local file system.
    * **Metadata:** Store document metadata (filename, type, size, upload date, associated employee/review) in the database.
    * **Security:** Implement access control checks based on user roles/permissions before allowing upload, download, or deletion. Consider virus scanning upon upload. Use secure, time-limited URLs (SAS tokens) for downloads if applicable.
* **Calendar Integration:**
    * **Internal View:** Display leave requests/holidays on an internal Blazor calendar component.
    * **External Sync (Optional/Future):** If integration with user calendars (Outlook, Google) is required:
        * Use OAuth 2.0 for authorization (requesting calendar permissions).
        * Utilize respective APIs (Microsoft Graph API, Google Calendar API). This adds significant complexity and requires secure management of refresh tokens.
        * Alternatively, offer iCal (.ics) export/subscription links for approved leave. Start with internal display and iCal export.
* **Error Handling:** Implement global exception handling middleware. Log exceptions with details. Provide user-friendly error feedback in the UI.
* **Logging Strategy:** Log key events, application flow, errors, and security-relevant actions (e.g., login attempts, permission changes). Include correlation IDs for tracing requests across services/layers.
* **Configuration Management:** Use `appsettings.json` per environment, environment variables, and secure secret management (User Secrets in Dev, Azure Key Vault, AWS Secrets Manager, HashiCorp Vault in Staging/Prod).

**6. Non-Functional Requirements**

* **Security:** Enforce HTTPS. Implement security headers (CSP, HSTS). Protect against common web vulnerabilities (XSS, CSRF - Blazor helps). Regularly scan dependencies. Secure configuration and secrets. Perform security code reviews and penetration testing.
* **Performance:** Optimize database queries (indexing, avoid N+1). Use asynchronous programming (`async`/`await`) correctly. Implement caching. Monitor application performance.
* **Maintainability:** Adhere to SOLID principles. Write clean, well-documented code. Implement comprehensive unit and integration tests.
* **Testability:** Ensure components and services are designed for testability (DI, interfaces, mocking). Aim for high test coverage for business logic.
* **Reliability:** Implement robust error handling and logging. Use durable storage for documents and data.

**7. Development Practices & DevOps**

* **Source Control:** Git (using a platform like GitHub, Azure Repos, GitLab).
* **Branching Strategy:** GitFlow or GitHub Flow (adapt based on team size and release cadence).
* **Code Reviews:** Mandatory pull requests with peer reviews.
* **Automated Testing:** Unit Tests (xUnit/NUnit), Integration Tests (using `WebApplicationFactory`, Testcontainers). Consider End-to-End tests (Playwright, Selenium) for critical user flows.
* **CI/CD:** Implement automated build, test, and deployment pipelines (GitHub Actions, Azure Pipelines, Jenkins).
* **Infrastructure as Code (IaC):** Use tools like Terraform or Bicep/ARM templates to manage cloud infrastructure provisioning.
* **Containerization:** Dockerize the application and Identity Provider for consistent environments and deployment.

**8. Evolution & Future Considerations**

* **Microservices:** While starting monolithic/modular, design boundaries clearly to facilitate potential future extraction into microservices if scalability demands it.
* **Reporting:** Plan for reporting needs (consider dedicated reporting tools or data warehousing if complexity grows).
* **Mobile App:** If a mobile app is envisioned, ensure APIs are designed cleanly for consumption.

These guidelines provide a foundational technical direction. Detailed design documents for specific modules and features will be necessary as development progresses. Continuous review and adaptation of these guidelines may be required based on project discoveries and evolving requirements.