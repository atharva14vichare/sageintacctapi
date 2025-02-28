using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using SageIntacctApi.Models;

namespace SageIntacctApi.Services;

public class SageIntacctService : ISageIntacctService
{
    private readonly SageIntacctConfig _config;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SageIntacctService> _logger;

    public SageIntacctService(IOptions<SageIntacctConfig> config, HttpClient httpClient, ILogger<SageIntacctService> logger) // Modified constructor
    {
        _config = config.Value;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetAccessTokenAsync(string authorizationCode)
    {
        _logger.LogInformation("Attempting to get access token..."); // Added log

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.intacct.com/ia/oauth/token");
            var content = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "redirect_uri", "http://localhost" }
            };

            if (_config.ClientId != null)
            {
                content.Add("client_id", _config.ClientId);
            }

            if (_config.ClientSecret != null)
            {
                content.Add("client_secret", _config.ClientSecret);
            }

            request.Content = new FormUrlEncodedContent(content);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Access Token Response: {responseContent}"); // Added log

            var json = JObject.Parse(responseContent);

            if (json["access_token"] != null)
            {
                return json["access_token"].ToString();
            }

            throw new Exception("Failed to retrieve access token from Sage Intacct API.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting access token."); // Added log
            throw;
        }
    }

    public async Task<string> GetVendorsAsync(string accessToken)
    {
        _logger.LogInformation("Attempting to get vendors..."); // Added log

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.intacct.com/api/v1/KerrConsulting-szhang/");
            request.Content = JsonContent.Create(new
            {
                request = new {
                    control = new { function = "get_list", controlid = "testControlId" },
                    operation = new { objectName = "VENDOR", fields = "*" } // Changed object to objectName
                }
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Get Vendors Response: {responseContent}"); // Added log

            return responseContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendors."); // Added log
            throw;
        }
    }
}