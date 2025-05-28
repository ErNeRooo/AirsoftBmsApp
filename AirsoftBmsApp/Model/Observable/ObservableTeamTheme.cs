using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public partial class ObservableTeamTheme : ObservableObject
    {
        [ObservableProperty]
        private Color backgroundColor;

        [ObservableProperty]
        private Color surfaceColor;

        [ObservableProperty]
        private Color titleColor;

        [ObservableProperty]
        private Color fontColor;
    }
}
