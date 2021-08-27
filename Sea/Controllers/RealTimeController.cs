using System;
using System.Windows.Threading;
using Sea.Models;
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
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromMinutes(1));

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

            _limiter.Do(() =>
            {
                _appContext.OrdersController.Refresh();
            });
        }
    }
}
