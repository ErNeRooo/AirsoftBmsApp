using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Account
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public int PlayerId { get; set; }
        public string? Email { get; set; }
    }
}
