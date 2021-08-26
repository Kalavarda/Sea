using System;
using Sea.Models.Geometry;

namespace Sea.Models
{
    public class Port
    {
        public PointF Position { get; set; }

        public void RaiseTradeFuel()
        {
            TradeFuel?.Invoke(this);
        }

        public event Action<Port> TradeFuel;
    }
}
