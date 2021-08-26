using System.Windows;
using System.Windows.Controls;
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
                    
                    _canvas.Children.Clear();
                    foreach (var port in _island.Ports)
                    {
                        var portControl = new PortControl { Port = port };
                        Canvas.SetLeft(portControl, port.Position.X - portControl.Width / 2);
                        Canvas.SetTop(portControl, port.Position.Y - portControl.Height / 2);
                        _canvas.Children.Add(portControl);
                    }
                }
            }
        }

        public IslandControl()
        {
            InitializeComponent();
        }
    }
}
