using Microsoft.AspNetCore.Mvc;
using SageIntacctApi.Services;
using System.Threading.Tasks;

namespace SageIntacctApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    private readonly ISageIntacctService _sageIntacctService;

    public VendorsController(ISageIntacctService sageIntacctService)
    {
        _sageIntacctService = sageIntacctService;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<string>> GetVendors()
    {   
        return new string[] { "Test 1", " Test 2" };
        // try
        // {
        //     var accessToken = await _sageIntacctService.GetAccessTokenAsync();
            
        //     var vendorsJson = await _sageIntacctService.GetVendorsAsync(accessToken);
        //     return Ok(vendorsJson);
        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(ex.Message);
        // }
    }
}