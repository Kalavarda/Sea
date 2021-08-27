﻿using Sea.Models.Geometry;

namespace Sea.Models.Interfaces
{
    public interface IPathFinder
    {
        Path Find(PointF source, PointF target);
    }
}
