using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class MockPlayerRestService : IPlayerRestService
    {
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
