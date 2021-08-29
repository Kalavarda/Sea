using System.Collections.Generic;

namespace Sea.Models.Interfaces
{
    public interface ITakeOrderController
    {
        /// <summary>
        /// Взяться за доставку указанной массы товара
        /// </summary>
        void TakeOrder(uint goodsId, float mass);

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
        decimal GetCost(uint goodsId, float mass);

        /// <summary>
        /// Какая масса влезет на судно
        /// </summary>
        float GetMaxAllowedMass();
    }
}
