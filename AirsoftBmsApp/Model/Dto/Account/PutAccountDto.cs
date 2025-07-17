using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Account
{
    public class PutAccountDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
