using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;

namespace Sea.Models.Geometry
{
    public interface IHasPosition
    {
        PointF Position { get; }
    }
}