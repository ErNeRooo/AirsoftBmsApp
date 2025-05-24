using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
        public string? Token { get; set; }
    }
}
