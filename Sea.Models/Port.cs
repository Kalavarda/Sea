using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Sea.Models
{
    public class Port : IHasPosition, IIdentifable
    {
        public uint Id { get; set; }

        public PointF Position { get; set; } = new PointF();

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
