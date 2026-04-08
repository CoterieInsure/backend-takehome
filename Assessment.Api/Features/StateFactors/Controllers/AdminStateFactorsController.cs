using Assessment.Api.Features.StateFactors.Models;
using Assessment.Api.Features.StateFactors.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Features.StateFactors.Controllers;

[ApiController]
[Route("api/v1/admin/state-factors")]
public class AdminStateFactorsController : ControllerBase
{
    private readonly IStateFactorService _service;

    public AdminStateFactorsController(IStateFactorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StateFactorResponse>>> GetAll()
    {
        var factors = await _service.GetAllAsync();
        var response = factors.Select(f => new StateFactorResponse
        {
            State = f.State,
            Factor = f.Factor
        });
        return Ok(response);
    }

    [HttpGet("{state}")]
    public async Task<ActionResult<StateFactorResponse>> Get(string state)
    {
        var factor = await _service.GetByStateAsync(state);
        if (factor == null)
        {
            return NotFound();
        }
        return new StateFactorResponse
        {
            State = factor.State,
            Factor = factor.Factor
        };
    }

    [HttpPost]
    public async Task<ActionResult<StateFactorResponse>> Create(CreateStateFactorRequest request)
    {
        var entity = new StateFactor
        {
            State = request.State,
            Factor = request.Factor
        };
        var created = await _service.CreateAsync(entity);
        var response = new StateFactorResponse
        {
            State = created.State,
            Factor = created.Factor
        };
        return CreatedAtAction(nameof(Get), new { state = created.State }, response);
    }

    [HttpPut("{state}")]
    public async Task<IActionResult> Update(string state, UpdateStateFactorRequest request)
    {
        var entity = new StateFactor
        {
            State = state,
            Factor = request.Factor
        };
        var success = await _service.UpdateAsync(state, entity);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{state}")]
    public async Task<IActionResult> Delete(string state)
    {
        var success = await _service.DeleteAsync(state);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
