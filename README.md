# Backend Take-Home Project
## Task
One of Coterie's core backend projects is our insurance quote rating engine and API. You'll use an Excel sheet to create a simplified rating engine and quote API.

In this repo is an Excel sheet with a simplified insurance rating algorithm. Your job is to translate that into a C# .NET Core API.

The cells shaded blue represent inputs that the API should take in as a JSON payload. Cells B6 through B9 each have a formula to be implemented to calculate their value (or they contain a hardcoded value). The tables to the right show the possible states and business inputs and their associated values. Cell B11 has a formula which is used to calculate the Total Premium, which is the result that the API should return.

## Example
If the API receives a payload of:
```
{
    revenue: 6000000,
    state: 'TX',
    business: 'Plumber'
}
```
It should respond with a payload of:
```
{
    premium: 11316
}
```

## Requirements
- .NET Core API with a `POST` endpoint that accepts and returns data per the above task description
- API can be run locally and tested using Postman or other similar tools
- Description of what features, functionality, etc. you would add next and how you would implement them - you shouldn't spend more than 3 hours on this project, so we want to know what you'd do next (and how you'd do it) if you had more time
- Use git and GitHub for version control
- **Have fun!** We're interested in seeing how you approach the challenge and how you solve problems with code. The goal is for you to be successful, so if you have any questions or something doesn't seem clear don't hesitate to ask. Asking questions and seeking clarification isn't a negative indicator about your skills - it shows you care and that you want to do well. Asking questions is *always* encouraged at Coterie, and our hiring process is no different.


## Deliverables Checklist
- [ ] API written in .NET Core
- [ ] API accepts `POST` and returns data per above requirements
- [ ] Repo README has instructions for running and testing the API
- [ ] Repo README has information about what you'd do next, per above requirements
- [ ] Create a new GitHub repo