using Sea.Models.Geometry;

namespace Sea.Models
{
    public class Ship
    {
        public PointF Position { get; set; }

        public SizeF Size { get; set; } = new SizeF { Width = 1, Height = 2 };
    }
}
