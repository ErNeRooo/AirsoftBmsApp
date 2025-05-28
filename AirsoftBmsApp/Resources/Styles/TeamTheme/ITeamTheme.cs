using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Resources.Styles.TeamTheme
{
    public interface ITeamTheme
    {
        Color BackgroundColor { get; }
        Color SurfaceColor { get; }
        Color TitleColor { get; }
        Color FontColor { get; }
    }

}
