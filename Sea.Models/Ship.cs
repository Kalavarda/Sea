using System;
using System.Text.Json.Serialization;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Sea.Models.Geometry;
using Sea.Models.Utils;

namespace Sea.Models
{
    public class Ship : IHasPosition
    {
        private const double MinSpeedDelta = 0.0001;
        private const double MinDirectionDelta = MathF.PI / 10000;

        private float _speed;
        private float _direction;
        public PointF Position { get; set; } = new PointF();

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

        public SizeF Size { get; set; }

        public Engine Engine { get; set; }

        [JsonIgnore]
        public float Speed
        {
            get => _speed;
            set
            {
                if (MathF.Abs(_speed - value) < MinSpeedDelta)
                    return;

                _speed = value;

                SpeedChanged?.Invoke(_speed);
            }
        }

        public event Action<float> SpeedChanged;

        public RangeF Fuel { get; set; }

        /// <summary>
        /// Масса товаров
        /// </summary>
        public RangeF GoodsMass { get; set; } = new RangeF();

        public OrderItem[] OrderItems { get; set; } = new OrderItem[0];

        public event Action<OrderItem> OrderItemAdded;
        public event Action<OrderItem> OrderItemRemoved;

        public void Add(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));
            OrderItems = OrderItems.Add(orderItem);
            OrderItemAdded?.Invoke(orderItem);
        }

        public void Remove(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));
            OrderItems = OrderItems.Remove(orderItem);
            OrderItemRemoved?.Invoke(orderItem);
        }
    }

    public class Engine
    {
        public RangeF Acceleration { get; set; }

        public RangeF Rotation { get; set; }

        /// <summary>
        /// Коэффициент расхода топлива
        /// </summary>
        public float FuelConsumption { get; set; }
    }

    public class OrderItem {
        private float _mass;
        
        public uint GoodsId { get; set; }

        public float Mass
        {
            get => _mass;
            set
            {
                if (MathF.Abs(_mass - value) < 0.001)
                    return;
                _mass = value;
                MassChanged?.Invoke(this);
            }
        }

        public event Action<OrderItem> MassChanged;
    }
}
