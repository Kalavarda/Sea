namespace Sea.Models.Controllers
{
    public interface IBuyFuelController
    {
        void Buy(float fuelCount);

        /// <summary>
        /// Максимально возможное количество для покупки в данный момент
        /// </summary>
        float GetMaxAvailableCount();
    }
}
