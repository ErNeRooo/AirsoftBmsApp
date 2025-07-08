using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Room
{
    public class PostRoomDto
    {
        public int MaxPlayers { get; set; }
        public string JoinCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
