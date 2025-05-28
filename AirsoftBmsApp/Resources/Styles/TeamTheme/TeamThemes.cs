using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Resources.Styles.TeamTheme
{
    public static class TeamThemes
    {
        public static readonly ITeamTheme UnderNoFlag = new UnderNoFlagTeamTheme();
        public static readonly ITeamTheme Red = new RedTeamTheme();

        private class UnderNoFlagTeamTheme : ITeamTheme
        {
            public Color BackgroundColor => Color.FromArgb("#4F4F4F");
            public Color SurfaceColor => Color.FromArgb("#1C1917");
            public Color TitleColor => Color.FromArgb("#D4D4D4");
            public Color FontColor => Color.FromArgb("#F5F5F5");
        }

        private class RedTeamTheme : ITeamTheme
        {
            public Color BackgroundColor => Color.FromArgb("#F0F0F0");
            public Color SurfaceColor => Color.FromArgb("#F0F0F0");
            public Color TitleColor => Color.FromArgb("#F0F0F0");
            public Color FontColor => Color.FromArgb("#F0F0F0");
        }
    }
}
