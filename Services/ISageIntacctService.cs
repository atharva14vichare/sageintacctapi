namespace SageIntacctApi.Services;

public interface ISageIntacctService
{
    Task<string> GetAccessTokenAsync(string authorizationCode);
    Task<string> GetVendorsAsync(string accessToken);
}