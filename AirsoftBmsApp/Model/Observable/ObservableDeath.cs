using AirsoftBmsApp.Model.Dto.Death;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable
{
    public partial class ObservableDeath : ObservableObject
    {
        [ObservableProperty]
        private int deathId;

        [ObservableProperty]
        private int locationId;

        [ObservableProperty]
        private int playerId;

        [ObservableProperty]
        private int battleId;

        [ObservableProperty]
        private double longitude;
        
        [ObservableProperty]
        private double latitude;
        
        [ObservableProperty]
        private double accuracy;
        
        [ObservableProperty]
        private Int16 bearing;
        
        [ObservableProperty]
        private DateTimeOffset time;

        public ObservableDeath(DeathDto deathDto)
        {
            DeathId = deathDto.DeathId;
            LocationId = deathDto.LocationId;
            PlayerId = deathDto.PlayerId;
            BattleId = deathDto.BattleId;
            Longitude = deathDto.Longitude;
            Latitude = deathDto.Latitude;
            Accuracy = deathDto.Accuracy;
            Bearing = deathDto.Bearing;
            Time = deathDto.Time;
        }
    }
}
