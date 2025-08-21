using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.KillRestService;

public class KillRestService(IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper, HttpClient client) : IKillRestService
{
    public async Task<(HttpResult result, KillDto kill)> PostAsync(PostKillDto postKillDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(postKillDto);

        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var room = await jsonHelper.DeserializeFromResponseAsync<KillDto>(response);

            return (new Success(), room);
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
