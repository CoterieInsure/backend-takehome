# Backend Take-Home Assessment

## Overview

This assessment has **two parts**:

| Part | What | Time |
|------|------|------|
| **Part 1: Coding** | Implement a premium rating API endpoint | ~2 hours |
| **Part 2: Written** | Answer 2 conceptual questions in `CONCEPTUAL_ASSESSMENT.md` | ~1 hour |

**Total time: ~3 hours.** If you finish faster, great. Please do not spend more than 4 hours. We would rather see a well-structured partial solution than a rushed complete one.

---

## Part 1: Coding Assessment

### Getting Started

**Prerequisites:** [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project Assessment.Api
```

The API will be available at `http://localhost:5000` with Swagger UI at `/swagger`.

### What We Provide

The scaffold includes the project structure, interfaces, models, middleware skeleton, a sample validator, a sample Bogus faker, rate tables in configuration, a pre-built carrier API client, and custom exception types. **You implement the business logic.**

| File | Status | Purpose |
|------|--------|---------|
| `Program.cs` | Modify | Register your services here |
| `Endpoints.cs` | Modify | Wire up your rating service |
| `Services/IRatingService.cs` | Implement | Interface your service must implement |
| `Services/ICarrierApiClient.cs` | Provided | Interface for the external carrier dependency |
| `Services/CarrierApiClient.cs` | **Pre-built** | Simulated carrier API — call it, don't modify it |
| `Exceptions/UnsupportedBusinessTypeException.cs` | **Pre-built** | Throw from your service for invalid business types |
| `Exceptions/InvalidStateException.cs` | **Pre-built** | Throw from your service for invalid/ineligible states |
| `Middleware/ExceptionHandler.cs` | Modify | Map the custom exceptions to HTTP status codes |
| `Validation/RatingRequestValidator.cs` | Modify | Expand validation rules |
| `appsettings.json` | Read-only | Rate tables and state mappings |

### The Task

Implement `POST /api/v1/rating` that:

1. Accepts a JSON payload with `business`, `revenue`, and `states`
2. Validates the request using FluentValidation
3. Calls the carrier API to validate eligibility for each business/state combination
4. Calculates the premium for each eligible state using the rate tables in `appsettings.json`
5. Returns the response as a bare DTO (no wrapper envelope)

**Premium formula:**

```
Premium = Revenue / 1000 × BasePremiumPerThousandRevenue × BusinessFactor × StateFactor
```

**Example request:**

```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "states": ["TX", "OH", "FLORIDA"]
}
```

**Example response:**

```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "premiums": [
    { "premium": 11316, "state": "TX" },
    { "premium": 12000, "state": "OH" },
    { "premium": 14400, "state": "FL" }
  ]
}
```

### Business Rules

- State abbreviation (`TX`) **and** full state name (`Texas`) are both accepted; always respond with the abbreviation. State name matching must be **case-insensitive** (e.g., `"FLORIDA"`, `"florida"`, and `"Florida"` all resolve to `"FL"`)
- Only **Plumber**, **Architect**, and **Programmer** are supported business types
- Only **Texas**, **Florida**, and **Ohio** are supported states
- If any value is **unsupported** (unknown business type or unknown state), reject the **entire request** with an error
- Revenue must be positive
- The carrier does not write all business/state combinations. When the carrier returns ineligible for a business/state combination, **omit that state from the response** — this is not an error. If all requested states are ineligible, return a successful response with an empty `premiums` array

### Error Behavior

| Scenario | HTTP Status | Trigger |
|----------|-------------|---------|
| Missing/empty fields, negative revenue | `400 Bad Request` | FluentValidation |
| Unknown business type (e.g., `"Baker"`) | `400 Bad Request` | `UnsupportedBusinessTypeException` |
| Unknown state (e.g., `"CA"`, `"California"`) | `400 Bad Request` | `InvalidStateException` |
| Carrier ineligible (e.g., Programmer in FL) | `200 OK` — omit that state from the `premiums` array | No error thrown |

**Example error response (validation failure):**

```json
{
  "errors": [
    { "propertyName": "Revenue", "errorMessage": "'Revenue' must be greater than '0'." }
  ]
}
```

**Example error response (unsupported business type):**

```json
{
  "error": "Business type 'Baker' is not supported."
}
```

**Example error response (unsupported state):**

```json
{
  "error": "State 'CA' is not supported or not eligible for the requested business type."
}
```

### Technical Requirements

| Requirement | Details |
|-------------|---------|
| **Minimal API endpoints** | Use `MapGroup`/`MapPost` with `[FromServices]` injection (scaffolded in `Endpoints.cs`) |
| **FluentValidation** | Expand the provided validator stub to cover all business rules |
| **Custom exception middleware** | Wire the provided exception types into `ExceptionHandler.cs` |
| **Async services** | All service methods must be `async Task<T>` |
| **DI registration** | Register your `RatingService` in `Program.cs` with appropriate lifetime |
| **ICarrierApiClient** | Call the pre-built client from your service; mock it in tests |
| **Unit tests** | Use NUnit + Moq + Bogus. A sample faker is provided in `Assessment.UnitTests/Fakers/` |

---

## Part 2: Written Assessment

Complete `CONCEPTUAL_ASSESSMENT.md`. Pick **one question from ONE of the 2 categories** (1 question total, 300–500 words). Prior insurance experience is helpful but not required — each question provides enough context to reason about the problem.

---

## Submission

1. Create a new **private** GitHub repo
2. Push your completed solution
3. Include your completed `CONCEPTUAL_ASSESSMENT.md`
4. Include instructions for running/testing if they differ from the above
5. Share access with the recruiter
