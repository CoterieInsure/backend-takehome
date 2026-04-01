# Backend Take-Home Assessment

## The Assignment

Build a simplified insurance premium **rating engine API** and a **mid-term endorsement endpoint**. You will implement business logic, validation, and tests for a .NET 8 minimal API.

The rate tables are provided in `appsettings.json` under the `RatingTables` section. Your job is to wire up the scaffold code into a working API that calculates premiums correctly.

## Time

Carve out **4-5 hours**. If you finish faster, great. If not, limit yourself and do not spend longer than 5 hours MAX. We would rather see a well-structured partial solution than a rushed complete one.

## Getting Started

**Prerequisites:** [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project Assessment.Api
```

The API will be available at `http://localhost:5000` with Swagger UI at `/swagger`.

## What We Provide

The scaffold includes the project structure, interfaces, request/response models, a middleware skeleton, a sample validator, a sample Bogus faker, and the rate tables in configuration. You implement the business logic.

| File | Purpose |
|---|---|
| `Program.cs` | Minimal hosting — register your services here |
| `Endpoints.cs` | Endpoint stubs with TODO markers — wire up your services |
| `Services/IRatingService.cs` | Interface your rating service must implement |
| `Services/ICarrierApiClient.cs` | Simulated external dependency — implement this |
| `Middleware/ExceptionHandler.cs` | Exception middleware — add your custom exception types |
| `Validation/RatingRequestValidator.cs` | Sample FluentValidation validator — expand it |
| `Validation/EndorsementRequestValidator.cs` | Empty validator — implement it |
| `Mapping/MappingProfile.cs` | Empty AutoMapper profile — define your mappings |
| `appsettings.json` | Rate tables and state mappings |

## Assessment Requirements

### Part 1: Rating Endpoint

Implement `POST /api/v1/rating` that:

- Accepts a JSON payload with `business`, `revenue`, and `states`
- Calculates the premium for each state using the rate tables in `appsettings.json`
- Returns bare DTO responses (no wrapper envelope)

**Premium formula:** `Premium = Revenue / 1000 * BasePremiumPerThousandRevenue * BusinessFactor * StateFactor`

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

### Part 2: Endorsement Endpoint

Implement `POST /api/v1/endorsements` that:

- Accepts a policy's original and updated revenue, business type, state, policy dates, and endorsement effective date
- Calculates the pro-rata premium adjustment for the remaining policy term
- Formula: `ProRataAdjustment = (NewAnnualPremium - OriginalAnnualPremium) * (DaysRemaining / TotalDays)`
- `DaysRemaining` = days from endorsement effective date to policy end date
- `TotalDays` = days from policy start date to policy end date

### Business Rules

- State abbreviation **and** full state name are both accepted; always respond with abbreviation
- Only **Plumber**, **Architect**, and **Programmer** are supported business types
- Only **Texas**, **Florida**, and **Ohio** are supported states
- If any value is unsupported, reject the entire request
- Revenue must be positive
- Endorsement effective date must fall within the policy term

### Technical Requirements

These reflect the patterns and libraries we use in production:

| Requirement | Details |
|---|---|
| **Minimal API endpoints** | Use `MapGroup`/`MapPost` with `[FromServices]` injection (already scaffolded in `Endpoints.cs`) |
| **FluentValidation** | Validate requests using `AbstractValidator<T>` classes — expand the provided stubs |
| **Custom exception middleware** | Create typed exceptions (e.g., `UnsupportedBusinessTypeException`) and handle them in `ExceptionHandler.cs` |
| **Async services** | All service methods must be `async Task<T>` |
| **AutoMapper** | Map between request DTOs, domain models, and response DTOs via `MappingProfile` |
| **DI registration** | Register services in `Program.cs` with appropriate lifetimes (`AddScoped`, `AddSingleton`) |
| **ICarrierApiClient** | Implement the simulated external carrier API (use `Task.Delay` to simulate latency) |
| **Unit tests** | Use NUnit + Moq + Bogus. A sample faker is provided in `Assessment.UnitTests/Fakers/` |

### Part 3: Conceptual Assessment

Complete the written assessment in `CONCEPTUAL_ASSESSMENT.md`. This is a separate evaluation of your ability to reason about complex software engineering problems in the insurance domain.

## Evaluation Criteria

| Category | Weight | What we look for |
|---|---|---|
| **Correctness** | 30% | Business rules implemented correctly, edge cases handled, premiums calculate accurately |
| **API Design** | 20% | Minimal API conventions, proper HTTP status codes, typed exceptions for error cases |
| **Architecture** | 20% | Service separation, DI lifetimes, async patterns, AutoMapper usage, configuration access |
| **Testing** | 20% | Meaningful unit tests, Bogus fakers for test data, Moq for dependencies, edge case coverage |
| **Code Quality** | 10% | Naming, organization, readability — no over-engineering |

## Submission

1. Create a new **private** GitHub repo
2. Push your completed solution
3. Add instructions for running and testing (if different from above)
4. Include your completed `CONCEPTUAL_ASSESSMENT.md`
5. Share access with the recruiter
