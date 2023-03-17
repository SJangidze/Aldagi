using Aldagi.Model;
using Application.Services.Policies;
using Application.Services.Policies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aldagi.Controllers;

[ApiController]
[Route("[controller]")]
public class PolicyController : ControllerBase
{
    private readonly PolicyService _policyService;

    public PolicyController(PolicyService policyService)
    {
        _policyService = policyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPolicies(int year, int month)
    {
        var result = await _policyService.GetMonthPolicies(year, month);

        return Ok(result);
    }

    [HttpGet("Quarter")]
    public async Task<IActionResult> GetPoliciesByQuarter(int year, int quarter)
    {
        var result = await _policyService.GetQuarterPolicies(year, quarter);

        return Ok(result);
    }
}
