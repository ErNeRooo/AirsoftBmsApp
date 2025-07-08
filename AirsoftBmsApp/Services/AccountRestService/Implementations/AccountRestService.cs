using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.AccountRestService.Implementations;

public class AccountRestService(HttpClient client, IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper) : IAccountRestService
{
    public async Task<(HttpResult result, AccountDto? account)> GetAsync(int accountId)
    {
        SetAuthorizationHeader();

        var response = await client.GetAsync($"id/{accountId}");

        if (response.IsSuccessStatusCode)
        {
            var account = await jsonHelper.DeserializeFromResponseAsync<AccountDto>(response);

            return (new Success(), account);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, AccountDto? account)> PutAsync(PutAccountDto accountDto, int accoundId)
    {
        SetAuthorizationHeader();

        var stringContent = jsonHelper.GetStringContent(accountDto);

        var response = await client.PutAsync($"id/{accoundId}", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var account = await jsonHelper.DeserializeFromResponseAsync<AccountDto>(response);

            return (new Success(), account);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<HttpResult> DeleteAsync(int accoundId)
    {
        SetAuthorizationHeader();

        var response = await client.DeleteAsync($"id/{accoundId}");

        if (response.IsSuccessStatusCode)
        {
            return new Success();
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new Failure(errorContent);
        }
    }

    public async Task<(HttpResult result, AccountDto? account)> SignUpAsync(SignUpAccountDto accountDto)
    {
        SetAuthorizationHeader();

        var stringContent = jsonHelper.GetStringContent(accountDto);

        var response = await client.PostAsync($"signup", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var account = await jsonHelper.DeserializeFromResponseAsync<AccountDto>(response);

            return (new Success(), account);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, AccountDto? account)> LogInAsync(LogInAccountDto accountDto)
    {
        SetAuthorizationHeader();

        var stringContent = jsonHelper.GetStringContent(accountDto);

        var response = await client.PostAsync($"login", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var account = await jsonHelper.DeserializeFromResponseAsync<AccountDto>(response);

            return (new Success(), account);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    private void SetAuthorizationHeader()
    {
        if (!string.IsNullOrEmpty(jwtTokenService.Token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenService.Token);
        }
        else
        {
            throw new Exception("No JWT token");
        }
    }
}
