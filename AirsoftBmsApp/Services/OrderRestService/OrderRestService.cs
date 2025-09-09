using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.OrderRestService;

public class OrderRestService(IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper, HttpClient client) : IOrderRestService
{
    public async Task<(HttpResult result, OrderDto Order)> PostAsync(PostOrderDto postOrderDto)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(postOrderDto);
         
        var response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var room = await jsonHelper.DeserializeFromResponseAsync<OrderDto>(response);

            return (new Success(), room);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<HttpResult> DeleteAsync(int id)
    {
        SetAuthorizationHeader();

        var response = await client.DeleteAsync($"id/{id}");

        if (response.IsSuccessStatusCode)
        {
            return (new Success());
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent));
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
