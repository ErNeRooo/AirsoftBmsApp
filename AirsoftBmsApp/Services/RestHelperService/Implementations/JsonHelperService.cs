using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Services.JwtTokenService;

namespace AirsoftBmsApp.Services.RestHelperService.Implementations
{
    public class JsonHelperService : IJsonHelperService
    {
        JsonSerializerOptions _serializeOptions;

        public JsonHelperService()
        {
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<T> DeserializeFromResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            var json = await response.Content.ReadAsStreamAsync();
            var room = await JsonSerializer.DeserializeAsync<T>(json, _serializeOptions);

            return room;
        }

        public StringContent GetStringContent(object dto)
        {
            var json = JsonSerializer.Serialize(dto, _serializeOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
