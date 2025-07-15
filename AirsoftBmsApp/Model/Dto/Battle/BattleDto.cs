using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Battle
{
    public class BattleDto
    {
        public string Name { get; set; }
        public int BattleId { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
    }
}
