# Conceptual Assessment

This written assessment evaluates your ability to reason about software engineering problems in the property & casualty (P&C) insurance domain. **No prior insurance experience is required** — each question provides enough context to reason about the problem.

## Instructions

- Pick **one question from ONE category** (1 question total)
- The answer should be **300–500 words**
- Focus on: clarity of thought, architectural reasoning, awareness of failure modes, and practical trade-offs

---

## Category A: Domain Modeling & State Machines (pick 1)

### A1. Policy Lifecycle

A commercial insurance policy goes through multiple phases: **quoting** (calculating a price), **binding** (activating coverage and committing the carrier), **issuance** (generating policy documents), **endorsement** (mid-term coverage changes), **renewal** (re-rating and extending for another term), and **cancellation** (terminating coverage early with pro-rata or short-rate refund calculations).

Describe how you would model this policy lifecycle as a state machine. What states and transitions exist? How would you enforce that only valid transitions occur in code? When a mid-term endorsement is applied, the policy effectively has a new "version" — how does versioning work, and how would you query the correct policy state for any point in time?

### A2. Claims-Made vs. Occurrence Coverage

Insurance policies can be written on two fundamentally different coverage triggers:
- **Occurrence-based** (e.g., General Liability): covers events that *happen* during the policy period, regardless of when the claim is reported — even years later.
- **Claims-made** (e.g., Professional Liability, Cyber): covers claims that are both *made and reported* during the policy period. A **retroactive date** limits how far back coverage extends, and an **Extended Reporting Period (ERP / "tail")** can be purchased to report claims after the policy expires.

How does this difference affect the data model for a policy administration system? What additional fields, date-based logic, and edge cases must you handle for claims-made policies? How would you determine whether a given claim is covered?

---

## Category B: Distributed Systems & Insurance (pick 1)

### B1. Policy Binding & Partial Failures

Policy binding is the most consequential API call in an insurance system. A single bind operation must: allocate a unique policy number, send a bind request to the carrier's policy administration system, charge the customer's payment method, create financial ledger entries, save an authoritative policy snapshot, and trigger asynchronous downstream effects (document generation, webhook delivery, CRM updates).

How would you design this endpoint to be **idempotent** (safe to retry)? What happens if payment succeeds but document generation fails? What if the carrier system is temporarily unavailable? Describe your approach to handling partial failures in this multi-step workflow.

### B2. Renewal Pipeline Architecture

A renewal pipeline must identify policies approximately 120 days before expiration, re-rate them at current rates (which may differ significantly from the expiring premium), give producers/agents a review window to adjust coverage, and auto-bind near expiration unless the producer opts out.

This is a long-running, multi-stage workflow spanning weeks with multiple decision points. How would you architect this system? What patterns would you consider (saga, choreography, orchestration, timer-based jobs) and why? How do you handle the "long tail" — the last 10–20% of policies that need disproportionate attention and are at risk of being missed?

---

## Your Answer

*(Write your answers below, clearly labeled with the category and question number you chose)*


