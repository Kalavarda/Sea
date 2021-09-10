using System;
using System.Windows;
using System.Windows.Controls;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Sea.Controls;
using Sea.Models;

namespace Sea.Controllers
{
    public class SoundController
    {
        private readonly MediaElement _shipMedia;
        private readonly Ship _ship;
        private readonly ViewInfo _viewInfo;

        public SoundController(MediaElement shipMedia, Ship ship, ViewInfo viewInfo)
        {
            _shipMedia = shipMedia ?? throw new ArgumentNullException(nameof(shipMedia));

            _ship = ship ?? throw new ArgumentNullException(nameof(ship));
            _viewInfo = viewInfo ?? throw new ArgumentNullException(nameof(viewInfo));

            _shipMedia.LoadedBehavior = MediaState.Manual;
            _shipMedia.MediaEnded += ShipMediaShipMediaEnded;

            _shipMedia.Source = new Uri(App.GetResourceFullFileName("Boat.mp3"));
            ShipMediaShipMediaEnded(this, new RoutedEventArgs());
            // _mediaElement.SpeedRatio = 0.25;

            ship.Engine.Acceleration.ValueChanged += Acceleration_ValueChanged;
            Acceleration_ValueChanged(ship.Engine.Acceleration);

            ship.Position.Changed += Position_Changed;
            Position_Changed(ship.Position);

            viewInfo.Zoom.ValueChanged += Zoom_ValueChanged;
            Zoom_ValueChanged(viewInfo.Zoom);

            viewInfo.Center.Changed += Center_Changed;
            Center_Changed(viewInfo.Center);
        }

        private void ShipMediaShipMediaEnded(object sender, RoutedEventArgs e)
        {
            _shipMedia.Position = TimeSpan.Zero;
            _shipMedia.Play();
        }

        private void Position_Changed(PointF shipPos)
        {
            _shipMedia.Volume = CalculateShipVolume(_viewInfo);
        }

        private void Center_Changed(PointF center)
        {
            _shipMedia.Volume = CalculateShipVolume(_viewInfo);
        }

        private void Zoom_ValueChanged(RangeF zoom)
        {
            _shipMedia.Volume = CalculateShipVolume(_viewInfo);
        }

        private float CalculateShipVolume(ViewInfo viewInfo)
        {
            var z = Math.Min(viewInfo.Zoom.Value / 100, 1);

            var distance = viewInfo.Center.DistanceTo(_ship.Position);
            var d = MathF.Min(1, 1 / MathF.Sqrt(distance));

            return z * d;
        }

        private void Acceleration_ValueChanged(RangeF acceleration)
        {
        }
    }
}
