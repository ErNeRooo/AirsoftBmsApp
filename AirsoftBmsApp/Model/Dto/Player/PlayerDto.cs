using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Player
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsDead { get; set; }
        public int? RoomId { get; set; }
        public int? TeamId { get; set; }
    }
}
