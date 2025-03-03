namespace SageIntacctApi.Services;

public interface ISageIntacctService
{
    Task<string> GetAccessTokenAsync(); // Add argument
    Task<string> GetVendorsAsync(string accessToken);
}