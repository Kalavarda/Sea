using System;
using System.Diagnostics;

namespace Sea.Models.Geometry
{
    [DebuggerDisplay("{X}; {Y}")]
    public class Point_F
    {
        private const float MinDelta = 0.001f;

        private float _x;
        private float _y;

        public static Point_F Zero { get; } = new Point_F { X = 0, Y = 0 };

        public float X
        {
            get => _x;
            set
            {
                if (MathF.Abs(value - _x) < MinDelta)
                    return;

                _x = value;

                XChanged?.Invoke();
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                if (MathF.Abs(value - _y) < MinDelta)
                    return;

                _y = value;

                YChanged?.Invoke();
            }
        }

        public event Action XChanged;
        public event Action YChanged;
        public event Action<Point_F> Changed;

        public void Set(float x, float y)
        {
            if (MathF.Abs(x - _x) < MinDelta && MathF.Abs(y - _y) < MinDelta)
                return;

            X = x;
            Y = y;

            Changed?.Invoke(this);
        }
    }

    public static class PointF_Extensions
    {
        public static float DistanceTo(this Point_F point1, Point_F point2)
        {
            if (point1 == null) throw new ArgumentNullException(nameof(point1));
            if (point2 == null) throw new ArgumentNullException(nameof(point2));
            var dx = point2.X - point1.X;
            var dy = point2.Y - point1.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public static Point_F Clone(this Point_F point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return new Point_F
            {
                X = point.X,
                Y = point.Y
            };
        }
    }
}
