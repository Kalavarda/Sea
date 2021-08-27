using Sea.Controllers;
using Sea.Factories;
using Sea.Models;
using Sea.Models.Impl;
using Sea.Models.Impl.Controllers;
using Sea.Models.Interfaces;
using Sea.Repositories;

namespace Sea
{
    public class AppContext
    {
        private Game _game;
        private IBuyFuelController _buyFuelController;
        private IOrdersController _ordersController;
        private readonly OrderCostCalculator _orderCostCalculator = new OrderCostCalculator();
        private readonly PathFinder _pathFinder = new PathFinder();

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                _game = value;
                
                _buyFuelController = null;
                _ordersController = null;
            }
        }

        public IGameRepository GameRepository { get; } = new FileGameRepository();

        public IGameFactory GameFactory { get; } = new GameFactory(new WorldFactory());

        public IBuyFuelController BuyFuelController
        {
            get
            {
                return _buyFuelController ??= new BuyFuelController(Game);
            }
        }

        public IOrdersController OrdersController
        {
            get
            {
                return _ordersController ??= new OrdersController(Game);
            }
        }

        public ITakeOrderController GetBuyController(Port port)
        {
            return new TakeOrderController(Game, port, _pathFinder, _orderCostCalculator);
        }
    }
}
