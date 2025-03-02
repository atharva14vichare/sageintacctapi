

    // [HttpGet]
    // public  ActionResult<IEnumerable<string>> GetVendors()
    // {   
    //     return new string[] { "Test 1", " Test 2" };
    //     // try
    //     // {
    //     //     var accessToken = await _sageIntacctService.GetAccessTokenAsync();
            
    //     //     var vendorsJson = await _sageIntacctService.GetVendorsAsync(accessToken);
    //     //     return Ok(vendorsJson);
    //     // }
    //     // catch (Exception ex)
    //     // {
    //     //     return BadRequest(ex.Message);
    //     // }
    // }

using Microsoft.AspNetCore.Mvc;
using SageIntacctApi.Services;
using Microsoft.Extensions.Logging; // Add this
using System.Threading.Tasks;

namespace SageIntacctApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    private readonly ISageIntacctService _sageIntacctService;
    private readonly ILogger<VendorsController> _logger; // Add this

    public VendorsController(ISageIntacctService sageIntacctService, ILogger<VendorsController> logger) // Modify constructor
    {
        _sageIntacctService = sageIntacctService;
        _logger = logger; // Add this
    }

    [HttpGet]
    public ActionResult<string> GetVendors()
    {
        _logger.LogInformation("GetVendors endpoint reached!"); // Add this
        return "Vendors endpoint reached!";
    }
}