using System;

namespace Sea.Models.Geometry
{
    public class PointF
    {
        private const float MinDelta = 0.001f;

        private float _x;
        private float _y;

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
        public event Action Changed;

        public void Set(float x, float y)
        {
            if (MathF.Abs(x - _x) < MinDelta && MathF.Abs(y - _y) < MinDelta)
                return;

            X = x;
            Y = y;

            Changed?.Invoke();
        }
    }
}
