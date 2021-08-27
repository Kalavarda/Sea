using System;
using Sea.Models;
using Sea.Models.Interfaces;

namespace Sea.Factories
{
    public class GameFactory: IGameFactory
    {
        private readonly IWorldFactory _worldFactory;

        public GameFactory(IWorldFactory worldFactory)
        {
            _worldFactory = worldFactory ?? throw new ArgumentNullException(nameof(worldFactory));
        }

        public Game Create(GameParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return new Game
            {
                World = _worldFactory.Create(parameters.WorldParameters),
                Economy = new Economy
                {
                    Money = parameters.Money,
                    FuelPrice = parameters.FuelPrice,
                    Goods = CreateGoods()
                }
            };
        }

        private static Goods[] CreateGoods()
        {
            var goods = new []
            {
                new Goods { Name = "Доски" },
                new Goods { Name = "Рыба" },
                new Goods { Name = "Консервы" },
                new Goods { Name = "Гвозди" },
                new Goods { Name = "Яблоки" },
                new Goods { Name = "Одежда" },
                new Goods { Name = "Аккумуляторы" }
            };
            for (uint i = 0; i < goods.Length; i++)
                goods[i].Id = i;
            return goods;
        }
    }
}
