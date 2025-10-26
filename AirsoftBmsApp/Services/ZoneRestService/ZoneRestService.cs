using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.ZoneRestService;

public class ZoneRestService(
    IJwtTokenService jwtTokenService, 
    IJsonHelperService jsonHelper, 
    HttpClient client
    ) : IZoneRestService
{
    public async Task<(HttpResult result, ZoneDto Zone)> PostAsync(PostZoneDto postZoneDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(postZoneDto);

        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var zone = await jsonHelper.DeserializeFromResponseAsync<ZoneDto>(response);

            return (new Success(), zone);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, ZoneDto Zone)> PutAsync(PutZoneDto putZoneDto, int zoneId)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(putZoneDto);

        var response = await client.PutAsync($"id/{zoneId}", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var zone = await jsonHelper.DeserializeFromResponseAsync<ZoneDto>(response);

            return (new Success(), zone);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<HttpResult> DeleteAsync(int ZoneId)
    {
        SetAuthorizationHeader();

        var response = await client.DeleteAsync($"id/{ZoneId}");

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
