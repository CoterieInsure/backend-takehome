using Assessment.Api.Features.BusinessFactors.Models;
using Assessment.Api.Features.BusinessFactors.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Features.BusinessFactors.Controllers;

[ApiController]
[Route("api/v1/admin/business-factors")]
public class AdminBusinessFactorsController : ControllerBase
{
    private readonly IBusinessFactorService _service;

    public AdminBusinessFactorsController(IBusinessFactorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessFactorResponse>>> GetAll()
    {
        var factors = await _service.GetAllAsync();
        var response = factors.Select(f => new BusinessFactorResponse
        {
            Business = f.Business,
            Factor = f.Factor
        });
        return Ok(response);
    }

    [HttpGet("{business}")]
    public async Task<ActionResult<BusinessFactorResponse>> Get(string business)
    {
        var factor = await _service.GetByBusinessAsync(business);
        if (factor == null)
        {
            return NotFound();
        }
        return new BusinessFactorResponse
        {
            Business = factor.Business,
            Factor = factor.Factor
        };
    }

    [HttpPost]
    public async Task<ActionResult<BusinessFactorResponse>> Create(CreateBusinessFactorRequest request)
    {
        var entity = new BusinessFactor
        {
            Business = request.Business,
            Factor = request.Factor
        };
        var created = await _service.CreateAsync(entity);
        var response = new BusinessFactorResponse
        {
            Business = created.Business,
            Factor = created.Factor
        };
        return CreatedAtAction(nameof(Get), new { business = created.Business }, response);
    }

    [HttpPut("{business}")]
    public async Task<IActionResult> Update(string business, UpdateBusinessFactorRequest request)
    {
        var entity = new BusinessFactor
        {
            Business = business,
            Factor = request.Factor
        };
        var success = await _service.UpdateAsync(business, entity);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{business}")]
    public async Task<IActionResult> Delete(string business)
    {
        var success = await _service.DeleteAsync(business);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
