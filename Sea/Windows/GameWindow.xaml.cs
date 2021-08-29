using System;
using System.Windows;
using System.Windows.Controls;
using Sea.Controllers;
using Sea.Controls;

namespace Sea.Windows
{
    public partial class GameWindow
    {
        private readonly AppContext _appContext;

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(AppContext appContext): this()
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            _worldControl.World = _appContext.Game.World;

            _infoBar.Game = _appContext.Game;
            _infoBar.WorldControl = _worldControl;

            new RealTimeController(_appContext);
            new SoundController(_shipMedia, appContext.Game.World.Ship, _worldControl.ViewInfo);

            Loaded += GameWindow_Loaded;
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new TradeController(_appContext, this);
            /*
            var player = new SoundPlayer(@"C:\_\08\28\Sounds\boat-outboard-motor-01.wav");
            player.Load();
            player.PlayLooping();
            */

            ShowToolWindow(new ShipDashboard { Ship = _appContext.Game.World.Ship }, 300, 300, string.Empty);

            var ordersControl = new OrdersControl(_appContext);
            new ShowPathController(ordersControl, _worldControl.PathCanvas);
            ShowToolWindow(ordersControl, 300, 300, "Заказы");
        }

        internal void ShowToolWindow(UserControl content, int width, int height, string title)
        {
            new Window
            {
                Content = content,
                Owner = this,
                ShowInTaskbar = false,
                Width = width,
                Height = height,
                WindowStyle = WindowStyle.ToolWindow,
                Title = title
            }.Show();
        }
    }
}
