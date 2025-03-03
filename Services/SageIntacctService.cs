using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SageIntacctApi.Models;

namespace SageIntacctApi.Services;

public class SageIntacctService : ISageIntacctService
{
    private readonly SageIntacctConfig _config;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SageIntacctService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public SageIntacctService(IOptions<SageIntacctConfig> config, HttpClient httpClient, ILogger<SageIntacctService> logger, IHttpClientFactory httpClientFactory)
    {
        _config = config.Value;
        _httpClient = httpClient;
        _logger = logger;
        _httpClientFactory = httpClientFactory;


    }

    public async Task<string> GetAccessTokenAsync()
    {
        _logger.LogInformation("Attempting to get access token...");

        if (string.IsNullOrEmpty(_config.AuthToken))
        {
            throw new Exception("Access token is not configured.");
        }

        return _config.AuthToken;

    }

    public async Task<string> GetVendorsAsync(string AuthToken)
    {
        _logger.LogInformation("Attempting to get vendors...");
        
        

        try
        {

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+AuthToken);
            var Key = "111";
            var request = await client.GetAsync("https://api.intacct.com/ia/api/v1/objects/accounts-payable/vendor/" + Key);
            var response = await request.Content.ReadAsStringAsync();


            //_logger.LogInformation($"Get Vendors Response: {response}"); 

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendors.");
            throw;
        }
    }
}