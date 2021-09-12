using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Sea.Models.Interfaces
{
    public interface IPathFinder
    {
        Path Find(PointF source, PointF target);

        Path Find(IHasPosition source, IHasPosition target);
    }
}
