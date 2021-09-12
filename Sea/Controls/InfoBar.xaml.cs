using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Sea.Models;
using Sea.Models.Utils;

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

                    _game.World.Ship.GoodsMass.ValueChanged += GoodsMass_ValueChanged;
                    GoodsMass_ValueChanged(_game.World.Ship.GoodsMass);

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
                    MouseWorldPosition_Changed(_worldControl.MouseWorldPosition);
                }
            }
        }

        public InfoBar()
        {
            InitializeComponent();
        }

        private void Economy_MoneyChanged()
        {
            _tbMoney.Text = _game.Economy.Money.ToStr();
        }

        private void GoodsMass_ValueChanged(RangeF mass)
        {
            _tbGoodsMass.Text = $"{mass.Value.ToStr()} / {mass.Max.ToStr()}";
        }

        private void Fuel_ValueChanged(RangeF fuel)
        {
            _tbFuel.Text = $"{fuel.Value.ToStr()} / {fuel.Max.ToStr()}";
        }

        private void MouseWorldPosition_Changed(PointF mousePos)
        {
            _tbMouseWorldPos.Text = $"{MathF.Round(_worldControl.MouseWorldPosition.X):### ### ###}; {MathF.Round(_worldControl.MouseWorldPosition.Y):### ### ###}";
        }
    }
}
