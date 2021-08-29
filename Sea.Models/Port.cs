using System;
using Sea.Models.Geometry;

namespace Sea.Models
{
    public class Port : IHasPosition
    {
        public uint Id { get; set; }

        public PointF Position { get; set; }

        public void RaiseTradeFuel()
        {
            TradeFuel?.Invoke(this);
        }

        public event Action<Port> TradeFuel;


        public void RaiseTakeOrder()
        {
            TakeOrder?.Invoke(this);
        }

        public event Action<Port> TakeOrder;

        public void RaiseCompleteOrder()
        {
            CompleteOrder?.Invoke(this);
        }

        public event Action<Port> CompleteOrder;
    }
}
