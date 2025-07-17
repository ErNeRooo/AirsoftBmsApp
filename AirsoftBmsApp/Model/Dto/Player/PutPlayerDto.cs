using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Player
{
    public class PutPlayerDto
    {
        public string? Name { get; set; }
        public bool? IsDead { get; set; }
        public int? TeamId { get; set; }
    }
}
