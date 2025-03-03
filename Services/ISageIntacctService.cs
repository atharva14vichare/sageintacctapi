namespace SageIntacctApi.Services;

public interface ISageIntacctService
{
    Task<string> GetAccessTokenAsync();
    Task<string> GetVendorsAsync(string accessToken);
}