# Backend Coding Assessment - Premium Rate API

## Overview

This is a **collaborative pair programming session** that tests how you work with an existing code base. You will work through this assessment during a **live session** with Coterie software engineers.

You are encouraged to explore the project and familiarize yourself with the tasks before-hand. Please make sure to come prepared with a development environmnet already setup and able to run this code locally.

During the live session, we want to hear your thought process as you work. Talk through your approach, ask questions, and show us how you verify that your implementation meets the requirements.

### Prerequisites
- .NET 10.0 SDK
- Swagger is built into the project, but feel free to use another way of hitting REST endpoints (Postman, curl, Rider, etc)
- Optional: Your preferred AI assistant
  - If you use an AI assistant, only ask it targeted questions as-you-go instead of having it implement everything at once

### What's Provided
- SQLite database setup
- Admin API endpoints to manage configuration data
- Database models
- Existing service interfaces (`IBusinessFactorService`, `IStateFactorService`, `ICarrierEligibilityService`)
- Unit test project (`Assessment.UnitTests`) with `DatabaseTestFixture` base class

### Your Tasks
1. Populate the database using the admin endpoints
2. Implement the `POST /api/v1/rates` endpoint that calculates premiums
3. Store the rates in the database
4. Write unit tests

### Implementation Guidelines
- **Reuse existing services**: Use `IBusinessFactorService`, `IStateFactorService`, and `ICarrierEligibilityService` via dependency injection rather than querying non-rate tables directly
- **Follow existing patterns**: Review the provided admin controllers and services to understand the project's architecture

---

## Task 1: Populate the Database

The SQLite database is automatically generated and starts empty. Use the admin endpoints to populate it with this data:

### Business Factors

| Business | Factor |
|----------|--------|
| Plumber | 1.25 |
| Programmer | 0.5 |

### State Factors

| State | Factor |
|-------|--------|
| TX | 0.943 |
| FL | 1.2 |

### Carrier Eligibility

| Business | TX | FL |
|----------|----|----|
| Plumber | ✓ | ✓ |
| Programmer | ✓ | ✗ |

---

## Task 2: Implement the Rate Endpoint

**Endpoint:** `POST /api/v1/rates`

### Request Format

```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "state": "TX"
}
```

**Field Requirements:**
- `business` (string, required): Must exist in BusinessFactors table
- `revenue` (number, required): Must be greater than 0
- `state` (string, required): Must exist in StateFactors table

### Response Formats

**Success (201 Created):**
```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "state": "TX",
  "premium": 28290
}
```
- Premium is an integer
- If business/state combination is ineligible, `premium` will be `null` (still returns 201)

**Validation Error (400 Bad Request):**

FluentValidation is already wired into every endpoint so that it returns 400s whenever validation fails.
All you need to do is implement the validator for your request type.

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Revenue": ["Revenue must be greater than 0"],
    "Business": ["Business type 'Baker' is not supported"]
  },
  "traceId": "..."
}
```

### Business Logic

**1. Validation:**
- All required fields must be present
- Business must exist in BusinessFactors (use `IBusinessFactorService.ExistsAsync`)
- Revenue must be > 0
- State must exist in StateFactors (use `IStateFactorService.ExistsAsync`)

**2. Carrier Eligibility Check:**
- Check eligibility using `ICarrierEligibilityService.GetByBusinessAndStateAsync`
- If `IsEligible = false` or no record exists, return `premium: null` (this is NOT an error)

**3. Premium Calculation:**

```
Premium = Revenue × BasePremium × BusinessFactor × StateFactor
```

Round the calculated premium to the nearest integer.

Where:
- **BasePremium**: 0.004 (hardcoded constant)
- **BusinessFactor**: From `IBusinessFactorService.GetByBusinessAsync`
- **StateFactor**: From `IStateFactorService.GetByStateAsync`

**Example:** Plumber with $6M revenue in TX:
```
Premium = 6,000,000 × 0.004 × 1.25 × 0.943 = 28,290
```

---

## Task 3: Store Rates to Database

Store each successful rate calculation (201 response) with:
- Request data (business, revenue, state)
- Calculated premium (or null if ineligible)
- Timestamp

---

## Task 4: Write Unit Tests

Add unit test coverage for your implementation in the provided test project.

Extend `DatabaseTestFixture` for tests that need a database.
