using Sea.Models.Geometry;
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
    }
}
