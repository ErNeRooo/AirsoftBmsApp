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
        public string JoinCode { get; set; }
        public string Password { get; set; }
    }
}
