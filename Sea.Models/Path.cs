using System;
using Sea.Models.Geometry;

namespace Sea.Models
{
    public class Path
    {
        public static readonly Path Empty = new Path(new PointF[0]);

        public PointF[] Points { get; }

        public float Length
        {
            get
            {
                if (Points.Length < 2)
                    return 0;

                var result = 0f;
                for (var i = 0; i < Points.Length - 1; i++)
                    result += Points[i].DistanceTo(Points[i + 1]);
                return result;
            }
        }

        public Path(PointF[] points)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }
    }
}
