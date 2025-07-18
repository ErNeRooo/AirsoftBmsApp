﻿using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Room
{
    public class RoomIncludingRelatedEntitiesDto
    {
        public int RoomId { get; set; }
        public int MaxPlayers { get; set; }
        public string JoinCode { get; set; }
        public PlayerDto? AdminPlayer { get; set; }
        public BattleDto Battle { get; set; }
        public List<PlayerDto> Players { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}
