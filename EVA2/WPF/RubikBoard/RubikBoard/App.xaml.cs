using System;
using System.Windows;
using RubikBoard.Model;
using RubikBoard.ViewModel;

namespace RubikBoard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        GameModel model;
        GameViewModel viewModel;
        MainWindow view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            model = new GameModel();
            model.GameOver += new EventHandler<GameEventArgs>(Model_GameOver);

            // nézemodell létrehozása
            viewModel = new GameViewModel(model);

            // nézet létrehozása
            view = new MainWindow();
            view.DataContext = viewModel;  // Összekapcsoljuk a nézetet és a nézetmodellt
            view.Show();
        }

        private void Model_GameOver(object sender, GameEventArgs e)
        {
            viewModel.Timer.Stop();
            MessageBox.Show("Fuck yeah!",
                            "Rubik tábla",
                            MessageBoxButton.OK,
                            MessageBoxImage.Asterisk);

            viewModel.SetUpGame(viewModel.Size);
        }
    }
}
