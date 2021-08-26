using System;
using Sea.Models;

namespace Sea.Controls
{
    public partial class InfoBar
    {
        private Game _game;
        private WorldControl _worldControl;

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                _game = value;

                if (_game != null)
                {
                    _game.World.Ship.Fuel.ValueChanged += Fuel_ValueChanged;
                    Fuel_ValueChanged(_game.World.Ship.Fuel);

                    _game.Economy.MoneyChanged += Economy_MoneyChanged;
                    Economy_MoneyChanged();
                }
            }
        }

        public WorldControl WorldControl
        {
            get => _worldControl;
            set
            {
                if (_worldControl == value)
                    return;

                _worldControl = value;

                if (_worldControl != null)
                {
                    _worldControl.MouseWorldPosition.Changed += MouseWorldPosition_Changed;
                    MouseWorldPosition_Changed();
                }
            }
        }

        public InfoBar()
        {
            InitializeComponent();
        }

        private void Economy_MoneyChanged()
        {
            _tbMoney.Text = Math.Round(_game.Economy.Money).ToString("### ### ###");
        }

        private void Fuel_ValueChanged(Models.RangeF fuel)
        {
            _tbFuel.Text = $"{MathF.Round(fuel.Value):### ### ###} / {MathF.Round(fuel.Max):### ### ###}";
        }

        private void MouseWorldPosition_Changed()
        {
            _tbMouseWorldPos.Text = $"{MathF.Round(_worldControl.MouseWorldPosition.X):### ### ###}; {MathF.Round(_worldControl.MouseWorldPosition.Y):### ### ###}";
        }
    }
}
