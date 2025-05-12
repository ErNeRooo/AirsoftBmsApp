using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Services.Abstractions;

namespace AirsoftBmsApp.Services
{
    public class PlayerRestService : IPlayerRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializeOptions;

        public PlayerRestService()
        {
            _client = new HttpClient();
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }


        public async Task<bool> RegisterPlayerAsync(string name)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> LogInToAccountAsync(string email, string password)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> SignUpAccountAsync(string name, string email, string password)
        {
            return await Task.FromResult(true);
        }
    }
}
