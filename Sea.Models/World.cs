using Kalavarda.Primitives.Geometry;

namespace Sea.Models
{
    public class World
    {
        public SizeF Size { get; set; }

        public Island[] Islands { get; set; }

        public Ship Ship { get; set; }
    }
}
