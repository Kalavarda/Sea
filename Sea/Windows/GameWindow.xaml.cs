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

            new TimeController(_appContext.Game.World);

            Loaded += GameWindow_Loaded;
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new TradeController(_appContext, this);

            ShowToolWindow(new ShipDashboard { Ship = _appContext.Game.World.Ship }, 300, 300);
        }

        internal void ShowToolWindow(UserControl content, int width, int height)
        {
            new Window
            {
                Content = content,
                Owner = this,
                ShowInTaskbar = false,
                Width = width,
                Height = height,
                WindowStyle = WindowStyle.ToolWindow
            }.Show();
        }
    }
}
