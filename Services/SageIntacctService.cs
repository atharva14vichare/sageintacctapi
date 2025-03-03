using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using SageIntacctApi.Models;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

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
         return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL2FwaS5pbnRhY2N0LmNvbSIsImlhdCI6MTc0MTAxMzY1NiwiZXhwIjoxNzQxMDU2ODU2LCJjbGllbnRJZCI6ImNiMmY5YzY1ZDMxY2VjMGQ4NGM5LmFwcC5zYWdlLmNvbSIsImNueUlkIjoiS2VyckNvbnN1bHRpbmctc3poYW5nIiwiY255S2V5IjoiMTc1NDkwIiwidXNlcklkIjoicHNfY29uZmlnX1NhbWV0IiwidXNlcktleSI6Ijg0Iiwic2Vzc2lvbklkIjoicjQxNUxNSFE2S3NMUFFBVDV2R1RWNkNGcXdyNE82X05lU3pCMGtVcEN6MEFFX2J4a3dPOW1La0kiLCJlbnRpdHlLZXkiOiIiLCJlbnRpdHlJZCI6IiJ9.YV7VOHiTtc2ezZA_Jq_s6fFMo9TUgDwSD5WHWCb_Mhw" ;

        // try
        // {
        //     var request = new HttpRequestMessage(HttpMethod.Post, "https://api.intacct.com/ia/oauth/token");
        //     var content = new Dictionary<string, string>
        //     {
        //         { "grant_type", "authorization_code" },
        //         { "code", authorizationCode },
        //         { "redirect_uri", "http://localhost:5118/callback" }, // Ensure this matches your registered redirect URI
        //         { "client_id", _config.ClientId },
        //         { "client_secret", _config.ClientSecret }
        //     };

        //     request.Content = new FormUrlEncodedContent(content);

        //     var response = await _httpClient.SendAsync(request);
        //     response.EnsureSuccessStatusCode();

        //     var responseContent = await response.Content.ReadAsStringAsync();

        //     _logger.LogInformation($"Access Token Response: {responseContent}");

        //     var json = JObject.Parse(responseContent);

        //     if (json["access_token"] != null)
        //     {
        //         return json["access_token"].ToString();
        //     }

        //     throw new Exception("Failed to retrieve access token from Sage Intacct API.");
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "Error getting access token.");
        //     throw;
        // }
    }

    public async Task<string> GetVendorsAsync(string accessToken)
    {
        _logger.LogInformation("Attempting to get vendors...");
        

        try
        {
            // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // var response = await _httpClient.GetAsync("https://api.intacct.com/ia/api/v1/objects/accounts-payable/vendor/111");
            // response.EnsureSuccessStatusCode();
            // var responseContent = await response.Content.ReadAsStringAsync();




            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+accessToken);
            var Key = "111";
            var request = await client.GetAsync("https://api.intacct.com/ia/api/v1/objects/accounts-payable/vendor/" + Key);
            var response = await request.Content.ReadAsStringAsync();


            _logger.LogInformation($"Get Vendors Response: {response}");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendors.");
            throw;
        }
    }
}