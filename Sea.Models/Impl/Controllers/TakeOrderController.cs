using System;
using System.Collections.Generic;
using System.Linq;
using Sea.Models.Geometry;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl.Controllers
{
    public class TakeOrderController: ITakeOrderController
    {
        internal const float MinPortDistance = 10;

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

        public void TakeOrder(uint goodsId, float mass)
        {
            if (mass <= 0)
                throw new Exception("Некорректное количество товара");

            var free = _game.World.Ship.GoodsMass.Max - _game.World.Ship.GoodsMass.Value;
            if (free < mass)
                throw new Exception("Перегруз");

            var distanceToShip = _game.World.Ship.Position.DistanceTo(_port.Position);
            if (distanceToShip > MinPortDistance)
                throw new Exception("Слишком большое расстояние до порта");

            var orderOption = _game.Economy.OrderOptions.First(oo => oo.SourcePortId == _port.Id && oo.GoodsId == goodsId);
            var destPort = _game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == orderOption.DestPortId);
            var path = _pathFinder.Find(destPort, _port);
            var orderDistance = path.Length;
            _game.Economy.Add(new Order
            {
                OrderOptionId = orderOption.Id,
                Cost = _orderCostCalculator.GetCost(mass, orderDistance),
                Mass = mass
            });
            _game.World.Ship.Add(new OrderItem { GoodsId = goodsId, Mass = mass });
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

            var port1 = _game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.SourcePortId);
            var port2 = _game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.DestPortId);
            var path = _pathFinder.Find(port1, port2);
            return path.Length;
        }

        public decimal GetCost(uint goodsId, float mass)
        {
            var distance = GetDistance(goodsId);
            return _orderCostCalculator.GetCost(mass, distance);
        }

        public float GetMaxAllowedMass()
        {
            return _game.World.Ship.GoodsMass.Max - _game.World.Ship.GoodsMass.Value;
        }
    }
}
