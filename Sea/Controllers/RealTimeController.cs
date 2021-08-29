using System;
using System.Windows.Threading;
using Sea.Utils;

namespace Sea.Controllers
{
    public class RealTimeController
    {
        private readonly AppContext _appContext;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private DateTime _prevTime = DateTime.MinValue;
        private static readonly TimeSpan _bigTimeSpan = TimeSpan.FromDays(1);
        private readonly ShipTimeController _shipTimeController;
        private readonly TimeLimiter _refreshOrdersLimiter = new TimeLimiter(TimeSpan.FromMinutes(1));
        private readonly TimeLimiter _saveGameLimiter = new TimeLimiter(TimeSpan.FromMinutes(1));
        private int _saveCounter;

        public RealTimeController(AppContext appContext)
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));

            _shipTimeController = new ShipTimeController(_appContext.Game.World.Ship);

            _timer.Interval = TimeSpan.FromSeconds(1 / 60d);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var dt = DateTime.Now - _prevTime;
            if (dt < _bigTimeSpan)
            {
                _shipTimeController.Process(dt);
            }
            _prevTime = DateTime.Now;

            _refreshOrdersLimiter.Do(() =>
            {
                _appContext.OrdersController.Refresh();
            });

            _saveGameLimiter.Do(() =>
            {
                if (_saveCounter > 1)
                    _appContext.SaveGame();

                _saveCounter++;
            });
        }
    }
}
