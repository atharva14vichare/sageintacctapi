using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using SageIntacctApi.Models;

namespace SageIntacctApi.Services;

public class SageIntacctService : ISageIntacctService
{
    private readonly SageIntacctConfig _config;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SageIntacctService> _logger;

    public SageIntacctService(IOptions<SageIntacctConfig> config, HttpClient httpClient, ILogger<SageIntacctService> logger)
    {
        _config = config.Value;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetAccessTokenAsync(string authorizationCode)
    {
        // Not used with XML gateway approach
        return "xml-gateway-used";
    }

    public async Task<string> GetVendorsAsync(string accessToken)
    {
        _logger.LogInformation("Getting vendors using XML gateway");
        
        try
        {
            // Create XML request document
            var xml = 
                $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
                    <control>
                        <senderid>XMLGateway</senderid>
                        <password></password>
                        <controlid>{Guid.NewGuid()}</controlid>
                        <uniqueid>false</uniqueid>
                        <dtdversion>3.0</dtdversion>
                    </control>
                    <operation>
                        <authentication>
                            <login>
                                <userid>{_config.UserId}</userid>
                                <password>{_config.Password}</password>
                                <companyid>{_config.CompanyId}</companyid>
                            </login>
                        </authentication>
                        <content>
                            <function controlid=""func{Guid.NewGuid()}"">
                                <get_list object=""VENDOR"" maxitems=""100"">
                                    <fields>*</fields>
                                </get_list>
                            </function>
                        </content>
                    </operation>
                </request>";

            _logger.LogInformation("Sending XML request to Sage Intacct");
                
            // Send XML request
            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await _httpClient.PostAsync("https://api.intacct.com/ia/xml/xmlgw.phtml", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation($"Response status: {response.StatusCode}");
            
            return responseContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendors");
            throw;
        }
    }
}