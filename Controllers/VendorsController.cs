using Microsoft.AspNetCore.Mvc;
using SageIntacctApi.Services;
using System.Threading.Tasks;

namespace SageIntacctApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    private readonly ISageIntacctService _sageIntacctService;

    public VendorsController(ISageIntacctService sageIntacctService)
    {
        _sageIntacctService = sageIntacctService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVendors(string authorizationCode)
    {
        try
        {
            var accessToken = await _sageIntacctService.GetAccessTokenAsync(authorizationCode);
            var vendorsJson = await _sageIntacctService.GetVendorsAsync(accessToken);
            return Ok(vendorsJson);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}