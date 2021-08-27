using System.Collections.Generic;

namespace Sea.Models.Interfaces
{
    public interface ITakeOrderController
    {
        void TakeOrder(uint goodsId, uint count);

        /// <summary>
        /// Список видов, которые можно купить
        /// </summary>
        IEnumerable<Goods> GetAvailableGoods();

        /// <summary>
        /// Расстояние доставки (по прямой)
        /// </summary>
        float GetDistance(uint goodsId);

        /// <summary>
        /// Плата за доставку указанного кол-ва товаров
        /// </summary>
        decimal GetCost(uint goodsId, uint count);
    }
}
