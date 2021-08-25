using System;
using Sea.Models;

namespace Sea.Controllers
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

            _ship.Speed += seconds * _ship.Engine.Acceleration.Value;
            _ship.Direction += seconds * _ship.Engine.Rotation.Value;

            var x = _ship.Position.X + seconds * _ship.Speed * MathF.Cos(_ship.Direction);
            var y = _ship.Position.Y + seconds * _ship.Speed * MathF.Sin(_ship.Direction);
            _ship.Position.Set(x, y);
        }
    }
}
