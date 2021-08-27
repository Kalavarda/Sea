namespace Sea.Models
{
    /// <summary>
    /// Принятый заказ
    /// </summary>
    public class Order
    {
        public uint OrderOptionId { get; set; }

        /// <summary>
        /// Сколько единиц товара уже доставлено
        /// </summary>
        public uint DeliveredCount { get; set; }

        /// <summary>
        /// Сколько единиц товара нужно доставить
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Награда за полное выполнение заказа
        /// </summary>
        public decimal Cost { get; set; }
    }

    public class OrderOption
    {
        public uint Id { get; set; }

        public uint GoodsId { get; set; }

        public uint SourcePortId { get; set; }

        public uint DestPortId { get; set; }
    }
}
