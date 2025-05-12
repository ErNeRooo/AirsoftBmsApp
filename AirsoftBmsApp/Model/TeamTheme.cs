using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public class TeamTheme : INotifyPropertyChanged
    {
        private Color _backgroundColor;
        private Color _surfaceColor;
        private Color _titleColor;
        private Color _fontColor;

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SurfaceColor
        {
            get => _surfaceColor;
            set
            {
                if (_surfaceColor != value)
                {
                    _surfaceColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color TitleColor
        {
            get => _titleColor;
            set
            {
                if (_titleColor != value)
                {
                    _titleColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color FontColor
        {
            get => _fontColor;
            set
            {
                if (_fontColor != value)
                {
                    _fontColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
