using System;
using System.Windows;
using Sea.Models;

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
                    _ship_SpeedChanged();

                    _ship.Position.Changed += Position_Changed;
                    Position_Changed();

                    _ship.Engine.Rotation.MinChanged += Rotation_Changed;
                    _ship.Engine.Rotation.MaxChanged += Rotation_Changed;
                    Rotation_Changed(_ship.Engine.Rotation);
                }
            }
        }

        private void Acceleration_ValueChanged(RangeF acc)
        {
            _tbAccel.Text = MathF.Round(acc.Value, 2).ToString();
        }

        private void Rotation_Changed(RangeF rotation)
        {
            _sliderR.Minimum = rotation.Min * SliderRatio;
            _sliderR.Maximum = rotation.Max * SliderRatio;
        }

        private void _ship_SpeedChanged()
        {
            _tbSpeed.Text = MathF.Round(_ship.Speed, 1) + " м/с";
        }

        private void Acceleration_Changed(RangeF acceleration)
        {
            _sliderA.Minimum = acceleration.Min * SliderRatio;
            _sliderA.Maximum = acceleration.Max * SliderRatio;
        }

        private void Position_Changed()
        {
            _tbPosition.Text = $"{MathF.Round(_ship.Position.X):### ### ### ###} ; {MathF.Round(_ship.Position.Y):### ### ### ###}";
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
    }
}
