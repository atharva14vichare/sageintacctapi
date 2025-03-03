using Microsoft.AspNetCore.Mvc;
using SageIntacctApi.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SageIntacctApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<ActionResult<string>> GetVendors()
    {
        _logger.LogInformation("GetVendors endpoint reached!");

        try
        {
            var accessToken = await _sageIntacctService.GetAccessTokenAsync();
            _logger.LogInformation($"Access Token: {accessToken}");

            var vendorsJson = await _sageIntacctService.GetVendorsAsync(accessToken);
            _logger.LogInformation($"Vendors JSON: {vendorsJson}");

            return Ok(vendorsJson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving vendors.");
            return BadRequest(ex.Message);
        }
    }
}