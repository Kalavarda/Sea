using System;
using System.Collections.Generic;
using System.Linq;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl.Controllers
{
    public class OrdersController: IOrdersController
    {
        private readonly Game _game;
        private static readonly Random Rand = new Random();
        
        // Разница продажи/покупки
        private static readonly decimal Markup = (decimal)MathF.Sqrt(1.5f);

        public OrdersController(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Refresh()
        {
            var ports = _game.World.Islands.SelectMany(i => i.Ports).ToArray();

            var orderOptions = new List<OrderOption>();
            orderOptions.AddRange(_game.Economy.OrderOptions.Where(oo => _game.Economy.Orders.Any(o => o.OrderOptionId == oo.Id)));
            while (orderOptions.Count < ports.Length)
            {
                var p1 = ports[Rand.Next(ports.Length)];
                var p2 = ports[Rand.Next(ports.Length)];
                while (p2 == p1)
                    p2 = ports[Rand.Next(ports.Length)];

                var g = _game.Economy.Goods[Rand.Next(_game.Economy.Goods.Length)];
                while (orderOptions.Any(oo => oo.GoodsId == g.Id && (oo.SourcePortId == p1.Id || oo.SourcePortId == p2.Id || oo.DestPortId == p1.Id || oo.DestPortId == p2.Id)))
                    g = _game.Economy.Goods[Rand.Next(_game.Economy.Goods.Length)];

                orderOptions.Add(new OrderOption
                {
                    SourcePortId = p1.Id,
                    DestPortId = p2.Id,
                    GoodsId = g.Id
                });
            }

            var id = orderOptions.Max(oo => oo.Id) + 1;
            foreach (var orderOption in orderOptions)
                if (orderOption.Id == default)
                {
                    orderOption.Id = id;
                    id++;
                }

            _game.Economy.OrderOptions = orderOptions.ToArray();
        }
    }
}
