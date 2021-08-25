using System.Threading;
using System.Windows;
using Sea.Windows;

namespace Sea
{
    public partial class MainWindow
    {
        private readonly AppContext _appContext = new AppContext();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _appContext.Game = await _appContext.GameRepository.Load(CancellationToken.None);
            _btnContinue.IsEnabled = _appContext.Game != null;

            if (!_btnContinue.IsEnabled)
                OnNewClick(sender, e);
        }

        private void OnNewClick(object sender, RoutedEventArgs e)
        {
            var window = new NewGameWindow(_appContext) { Owner = this };
            if (window.ShowDialog() == true)
            {
                var gameWindow = new GameWindow(_appContext) { Owner = this };
                gameWindow.ShowDialog();
            }
        }

        private void OnContinueClick(object sender, RoutedEventArgs e)
        {
            var gameWindow = new GameWindow(_appContext) { Owner = this };
            gameWindow.ShowDialog();
        }
    }
}
