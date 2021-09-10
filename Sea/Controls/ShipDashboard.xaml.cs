using System.Windows;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Sea.Models;
using Sea.Models.Utils;

namespace Sea.Controls
{
    public partial class ShipDashboard
    {
        private const int SliderRatio = 20;
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
                    _ship.Engine.Acceleration.MinChanged += Acceleration_Changed;
                    _ship.Engine.Acceleration.MaxChanged += Acceleration_Changed;
                    Acceleration_Changed(_ship.Engine.Acceleration);
                    _ship.Engine.Acceleration.ValueChanged += Acceleration_ValueChanged;
                    Acceleration_ValueChanged(_ship.Engine.Acceleration);

                    _ship.SpeedChanged += _ship_SpeedChanged;
                    _ship_SpeedChanged(_ship.Speed);

                    _ship.Position.Changed += Position_Changed;
                    Position_Changed(_ship.Position);

                    _ship.Engine.Rotation.MinChanged += Rotation_Changed;
                    _ship.Engine.Rotation.MaxChanged += Rotation_Changed;
                    Rotation_Changed(_ship.Engine.Rotation);
                }
            }
        }

        private void Acceleration_ValueChanged(RangeF acc)
        {
            _tbAccel.Text = acc.Value.ToStr();
        }

        private void Rotation_Changed(RangeF rotation)
        {
            _sliderR.Minimum = rotation.Min * SliderRatio;
            _sliderR.Maximum = rotation.Max * SliderRatio;
        }

        private void _ship_SpeedChanged(float speed)
        {
            _tbSpeed.Text = _ship.Speed.ToStr() + " м/с";
        }

        private void Acceleration_Changed(RangeF acceleration)
        {
            _sliderA.Minimum = acceleration.Min * SliderRatio;
            _sliderA.Maximum = acceleration.Max * SliderRatio;
        }

        private void Position_Changed(PointF pos)
        {
            _tbPosition.Text = $"{_ship.Position.X.ToStr()} ; {_ship.Position.Y.ToStr()}";
        }

        public ShipDashboard()
        {
            InitializeComponent();
        }

        private void _sliderA_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ship.Engine.Acceleration.Value = (float)_sliderA.Value / SliderRatio;
        }

        private void _sliderR_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ship.Engine.Rotation.Value = (float)_sliderR.Value / SliderRatio;
        }

        private void OnAccelerationOffClick(object sender, RoutedEventArgs e)
        {
            _sliderA.Value = 0;
        }

        private void OnRotationOffClick(object sender, RoutedEventArgs e)
        {
            _sliderR.Value = 0;
        }

        private void OnAccelerationHalfClick(object sender, RoutedEventArgs e)
        {
            _sliderA.Value = _sliderA.Maximum / 2;
        }

        private void OnAccelerationFullClick(object sender, RoutedEventArgs e)
        {
            _sliderA.Value = _sliderA.Maximum;
        }
    }
}
