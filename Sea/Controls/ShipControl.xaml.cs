using System.Windows;
using System.Windows.Controls;
using Sea.Models;

namespace Sea.Controls
{
    public partial class ShipControl
    {
        private Ship _ship;

        public Ship Ship
        {
            get => _ship;
            set
            {
                if (_ship == value)
                    return;

                _ship = value;

                if (_ship != null)
                {
                    RecreateShape();

                    _ship.Position.Changed += Position_Changed;
                    Position_Changed();
                }
            }
        }

        private void RecreateShape()
        {
            Width = _ship.Size.Width;
            Height = _ship.Size.Height;

            var w = _ship.Size.Width / 2;
            var h = _ship.Size.Height / 4;

            _polygon.Points.Clear();
            _polygon.Points.Add(new Point(0, 0));
            _polygon.Points.Add(new Point(2 * w, 0));
            _polygon.Points.Add(new Point(2 * w, 3 * h));
            _polygon.Points.Add(new Point(w, 4 * h));
            _polygon.Points.Add(new Point(0, 3 * h));
        }

        private void Position_Changed()
        {
            this.Do(() =>
            {
                Canvas.SetLeft(this, _ship.Position.X - _ship.Size.Width / 2);
                Canvas.SetTop(this, _ship.Position.Y - _ship.Size.Height / 2);
            });
        }

        public ShipControl()
        {
            InitializeComponent();
        }
    }
}
