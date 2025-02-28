using Microsoft.AspNetCore.Mvc;
using SageIntacctApi.Services;

namespace SageIntacctApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    private readonly ISageIntacctService _sageIntacctService;
    private readonly ILogger<VendorsController> _logger;

    public VendorsController(ISageIntacctService sageIntacctService, ILogger<VendorsController> logger)
    {
        _sageIntacctService = sageIntacctService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetVendors()
    {
        try
        {
            // Direct XML call without OAuth
            var vendorsData = await _sageIntacctService.GetVendorsAsync("direct-xml-auth");
            return Ok(vendorsData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving vendors");
            return BadRequest(new { error = ex.Message });
        }
    }
}