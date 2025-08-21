using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.DeathRestService;

public class DeathRestService(IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper, HttpClient client) : IDeathRestService
{
    public async Task<(HttpResult result, DeathDto Death)> PostAsync(PostDeathDto postDeathDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(postDeathDto);

        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var room = await jsonHelper.DeserializeFromResponseAsync<DeathDto>(response);

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
