using System;
using System.Windows.Threading;
using Sea.Models;

namespace Sea.Controllers
{
    public class TimeController
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private DateTime _prevTime = DateTime.MinValue;
        private static readonly TimeSpan _bigTimeSpan = TimeSpan.FromDays(1);
        private readonly ShipTimeController _shipTimeController;

        public TimeController(World world)
        {
            _timer.Interval = TimeSpan.FromSeconds(1 / 60d);
            _timer.Tick += _timer_Tick;
            _shipTimeController = new ShipTimeController(world.Ship);
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
        }
    }
}
