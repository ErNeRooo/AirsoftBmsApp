using AirsoftBmsApp.Model.Dto.Battle;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable
{
    public partial class ObservableBattle : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int battleId;

        [ObservableProperty]
        private bool isActive;

        [ObservableProperty]
        private int roomId;

        public ObservableBattle()
        {
            
        }

        public ObservableBattle(BattleDto battleDto)
        {
            Name = battleDto.Name;
            BattleId = battleDto.BattleId;
            IsActive = battleDto.IsActive;
            RoomId = battleDto.RoomId;
        }
    }
}
