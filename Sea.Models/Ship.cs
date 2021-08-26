using System;
using System.Text.Json.Serialization;
using Sea.Models.Geometry;

namespace Sea.Models
{
    public class Ship
    {
        private const double MinSpeedDelta = 0.0001;
        private const double MinDirectionDelta = MathF.PI / 1000;

        private float _speed;
        private float _direction;
        public PointF Position { get; set; }

        /// <summary>
        /// Radians
        /// </summary>
        public float Direction
        {
            get => _direction;
            set
            {
                if (MathF.Abs(_direction - value) < MinDirectionDelta)
                    return;

                _direction = value;

                if (_direction > MathF.PI)
                    _direction -= 2 * MathF.PI;
                if (_direction < -MathF.PI)
                    _direction += 2 * MathF.PI;

                DirectionChanged?.Invoke();
            }
        }

        public event Action DirectionChanged;

        public SizeF Size { get; set; } = new SizeF { Width = 1, Height = 2 };

        public Engine Engine { get; set; } = new Engine();

        [JsonIgnore]
        public float Speed
        {
            get => _speed;
            set
            {
                if (MathF.Abs(_speed - value) < MinSpeedDelta)
                    return;

                _speed = value;

                SpeedChanged?.Invoke();
            }
        }

        public event Action SpeedChanged;

        public RangeF Fuel { get; set; } = new RangeF { Max = 10, Value = 10 };
    }

    public class Engine
    {
        public RangeF Acceleration { get; set; } = new RangeF(-0.25f, 1);

        public RangeF Rotation { get; set; } = new RangeF(-MathF.PI / 6, MathF.PI / 6);

        /// <summary>
        /// Коэффициент расхода топлива
        /// </summary>
        public float FuelConsumption { get; set; } = 0.01f;
    }
}
