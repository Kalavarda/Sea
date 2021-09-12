using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl
{
    public class PathFinder: IPathFinder
    {
        public Path Find(PointF source, PointF target)
        {
            // TODO: сделать нормально

            return new Path(new []{ source, target });
        }

        public Path Find(IHasPosition source, IHasPosition target)
        {
            return Find(source.Position, target.Position);
        }
    }
}
