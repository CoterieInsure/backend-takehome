# Conceptual Assessment

This written assessment evaluates your ability to reason about complex software engineering problems in the property & casualty (P&C) insurance domain. **No prior insurance experience is required** — each question provides enough context for you to reason about the problem.

## Instructions

- Pick **one question from each category** (4 questions total)
- Each answer should be **300-500 words**
- Diagrams are encouraged (embed images or use text-based diagrams)
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

This is a long-running, multi-stage workflow spanning weeks with multiple decision points. How would you architect this system? What patterns would you consider (saga, choreography, orchestration, timer-based jobs) and why? How do you handle the "long tail" — the last 10-20% of policies that need disproportionate attention and are at risk of being missed?

---

## Category C: Financial Accuracy & Reconciliation (pick 1)

### C1. Premium Audit

The premium charged at policy inception is an *estimate* based on projected exposures (e.g., projected annual payroll for Workers' Comp, projected revenue for General Liability). After the policy expires, the carrier audits the insured's actual exposures. If actual payroll was higher than projected, additional premium is owed. If lower, a refund is due. For loss-sensitive programs, retrospective adjustments can occur at 6-month intervals for 3-5 years after expiration.

Describe how you would design a system to handle premium audit adjustments. How do you maintain financial accuracy across these long time horizons (potentially 5+ years of adjustments for a single policy)? How do you handle disputes where the insured disagrees with the audit findings?

### C2. Endorsement Premium History

An endorsement changes coverage mid-term. The pro-rata premium adjustment is calculated as: `(Annual Premium Change) x (Days Remaining / Total Days in Policy)`. A single policy can have many endorsements over its term, each creating a new premium version.

Describe how you would calculate the correct total premium owed at any point in time, considering the full endorsement history. How would you handle an endorsement that is later reversed (e.g., the insured added a location then removed it)? What data structures and query patterns support this efficiently?

---

## Category D: Regulatory & Multi-Jurisdiction Complexity (pick 1)

### D1. State-by-State Variation

Insurance is regulated at the state level. Tax rates, surcharges, mandatory endorsement forms, available products, and even whether a product can be sold at all vary by jurisdiction. Some states require prior approval before using new rates or forms; others allow "file and use" or "use and file" regimes with different lead times. A single product expansion to a new state can require 30-90+ days of regulatory filing.

How would you design a system that handles this multi-jurisdiction complexity without creating an unmaintainable tangle of if/else branches or state-specific code paths? What patterns, data structures, or architectural approaches would you use? How do you handle the versioning problem — when a state approves new rates effective on a specific date, how do you ensure quotes use the correct rate table?

---

## Your Answers

*(Write your answers below, clearly labeled with the question number you chose)*

### Answer A_:



### Answer B_:



### Answer C_:



### Answer D_:

