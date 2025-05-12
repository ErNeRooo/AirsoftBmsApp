using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.Abstractions
{
    public interface IPlayerRestService
    {
        Task<bool> RegisterPlayerAsync(string name);
        Task<bool> LogInToAccountAsync(string email, string password);
        Task<bool> SignUpAccountAsync(string name, string email, string password);
    }
}
