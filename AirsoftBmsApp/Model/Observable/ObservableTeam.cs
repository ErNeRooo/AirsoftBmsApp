using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public partial class ObservableTeam : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private int roomId;

        [ObservableProperty]
        private int officerId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private ObservableCollection<ObservablePlayer> players;

        [ObservableProperty]
        private ObservableTeamTheme teamTheme;
    }

}
