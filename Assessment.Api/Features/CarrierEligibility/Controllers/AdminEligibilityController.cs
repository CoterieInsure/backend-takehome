using Assessment.Api.Features.CarrierEligibility.Models;
using Assessment.Api.Features.CarrierEligibility.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Features.CarrierEligibility.Controllers;

[ApiController]
[Route("api/v1/admin/eligibilities")]
public class AdminEligibilityController : ControllerBase
{
    private readonly ICarrierEligibilityService _service;

    public AdminEligibilityController(ICarrierEligibilityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarrierEligibilityResponse>>> GetAll()
    {
        var eligibilities = await _service.GetAllAsync();
        var response = eligibilities.Select(e => new CarrierEligibilityResponse
        {
            Id = e.Id,
            Business = e.Business,
            State = e.State,
            IsEligible = e.IsEligible
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarrierEligibilityResponse>> Get(int id)
    {
        var eligibility = await _service.GetByIdAsync(id);
        if (eligibility == null)
        {
            return NotFound();
        }
        return new CarrierEligibilityResponse
        {
            Id = eligibility.Id,
            Business = eligibility.Business,
            State = eligibility.State,
            IsEligible = eligibility.IsEligible
        };
    }

    [HttpPost]
    public async Task<ActionResult<CarrierEligibilityResponse>> Create(CreateCarrierEligibilityRequest request)
    {
        var entity = new Models.CarrierEligibility
        {
            Business = request.Business,
            State = request.State,
            IsEligible = request.IsEligible
        };
        var created = await _service.CreateAsync(entity);
        var response = new CarrierEligibilityResponse
        {
            Id = created.Id,
            Business = created.Business,
            State = created.State,
            IsEligible = created.IsEligible
        };
        return CreatedAtAction(nameof(Get), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCarrierEligibilityRequest request)
    {
        var entity = new Models.CarrierEligibility
        {
            Id = id,
            Business = request.Business,
            State = request.State,
            IsEligible = request.IsEligible
        };
        var success = await _service.UpdateAsync(id, entity);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
