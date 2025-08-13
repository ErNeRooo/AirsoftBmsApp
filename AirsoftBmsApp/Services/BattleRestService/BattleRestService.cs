using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.BattleRestService;

public class BattleRestService(IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper, HttpClient client) : IBattleRestService
{
    public async Task<(HttpResult result, BattleDto battle)> PostAsync(PostBattleDto battleDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(battleDto);

        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var room = await jsonHelper.DeserializeFromResponseAsync<BattleDto>(response);

            return (new Success(), room);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, BattleDto battle)> PutAsync(PutBattleDto battleDto, int battleId)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(battleDto);

        var response = await client.PutAsync($"id/{battleId}", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var room = await jsonHelper.DeserializeFromResponseAsync<BattleDto>(response);

            return (new Success(), room);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<HttpResult> DeleteAsync(int battleId)
    {
        SetAuthorizationHeader();

        var response = await client.DeleteAsync($"id/{battleId}");

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
