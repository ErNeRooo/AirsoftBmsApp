using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.MapPingRestService;

public class MapPingRestService(IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper, HttpClient client) : IMapPingRestService
{
    public async Task<(HttpResult result, MapPingDto MapPing)> PostAsync(PostMapPingDto postMapPingDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(postMapPingDto);

        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var mapPing = await jsonHelper.DeserializeFromResponseAsync<MapPingDto>(response);

            return (new Success(), mapPing);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<HttpResult> DeleteAsync(int mapPingId)
    {
        SetAuthorizationHeader();

        var response = await client.DeleteAsync($"id/{mapPingId}");

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
