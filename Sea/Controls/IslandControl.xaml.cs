using System.Windows;
using Sea.Models;

namespace Sea.Controls
{
    public partial class IslandControl
    {
        private Island _island;

        public Island Island
        {
            get => _island;
            set
            {
                if (_island == value)
                    return;

                _island = value;

                if (_island != null)
                {
                    _polygon.Points.Clear();
                    foreach (var p in _island.Points)
                        _polygon.Points.Add(new Point(p.X, p.Y));
                }
            }
        }

        public IslandControl()
        {
            InitializeComponent();
        }
    }
}
