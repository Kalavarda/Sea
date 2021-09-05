using System;

namespace Sea.Models.Impl.Controllers
{
    public class ShipTimeController
    {
        private readonly Ship _ship;

        public ShipTimeController(Ship ship)
        {
            _ship = ship ?? throw new ArgumentNullException(nameof(ship));
        }

        public void Process(TimeSpan delta)
        {
            var seconds = (float)delta.TotalSeconds;
            var massCoeff = 1 - 0.9f * _ship.GoodsMass.ValueN;

            var waterResistance = 0.5f * _ship.Speed; // сопротивление среды
            waterResistance *= 1 + _ship.GoodsMass.ValueN;

            var a = _ship.Engine.Acceleration.Value - waterResistance;
            a *= massCoeff;

            _ship.Speed += seconds * a;
            _ship.Direction += seconds * _ship.Engine.Rotation.Value * massCoeff;

            var x = _ship.Position.X + seconds * _ship.Speed * MathF.Cos(_ship.Direction);
            var y = _ship.Position.Y + seconds * _ship.Speed * MathF.Sin(_ship.Direction);
            _ship.Position.Set(x, y);

            _ship.Fuel.Value -= seconds * MathF.Pow(MathF.Abs(_ship.Engine.Acceleration.Value) * _ship.Engine.FuelConsumption, 1.5f);
            if (_ship.Fuel.Value <= 0.001)
                _ship.Engine.Acceleration.Value = 0;
        }
    }
}
