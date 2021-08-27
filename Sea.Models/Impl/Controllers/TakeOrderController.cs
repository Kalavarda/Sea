using System;
using System.Collections.Generic;
using System.Linq;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl.Controllers
{
    public class TakeOrderController: ITakeOrderController
    {
        private readonly Game _game;
        private readonly Port _port;
        private readonly IPathFinder _pathFinder;
        private readonly IOrderCostCalculator _orderCostCalculator;

        public TakeOrderController(Game game, Port port, IPathFinder pathFinder, IOrderCostCalculator orderCostCalculator)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _pathFinder = pathFinder ?? throw new ArgumentNullException(nameof(pathFinder));
            _orderCostCalculator = orderCostCalculator ?? throw new ArgumentNullException(nameof(orderCostCalculator));
        }

        public void TakeOrder(uint goodsId, uint count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> GetAvailableGoods()
        {
            var result = _game.Economy.OrderOptions
                .Where(oo => oo.SourcePortId == _port.Id);
            if (_game.Economy.Orders.Any())
                result = result.Where(oo => _game.Economy.Orders.All(o => o.OrderOptionId != oo.Id));
            return result
                .Select(oo => _game.Economy.Goods.First(g => g.Id == oo.GoodsId));
        }

        public float GetDistance(uint goodsId)
        {
            var option = _game.Economy.OrderOptions
                .Where(oo => oo.GoodsId == goodsId)
                .First(oo => oo.SourcePortId == _port.Id);

            var p1 = _game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.SourcePortId);
            var p2 = _game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.DestPortId);
            var path = _pathFinder.Find(p1.Position, p2.Position);
            return path.Length;
        }

        public decimal GetCost(uint goodsId, uint count)
        {
            var distance = GetDistance(goodsId);
            return _orderCostCalculator.GetCost(count, distance);
        }
    }
}
