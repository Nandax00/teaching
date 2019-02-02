using System;
using System.Windows;

/// <summary>
/// Ez az osztály a view code-behindja. Itt érdemes létrehozni modellt, a nézetmodellt, a nézetet és a perzisztenciát,
/// és itt adjuk meg, mi történjen, amikor kiváltódnak a nézetmodellben definiált események.
/// </summary>

namespace SampleWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Model model;
        private ViewModel viewModel;
        private MainWindow view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            model = new Model();
            viewModel = new ViewModel(model);
            view = new MainWindow();
            
            // Step 4 / a
            viewModel.ChangeColorEvent += new EventHandler(ViewModel_ChangeColor); //eseménykezelőt adunk át az eseménynek
            viewModel.QuitEvent += new EventHandler(ViewModel_Quit);
            view.DataContext = viewModel; //a DataContext adattag ad kontextust a nézetnek, ez a nézetmodell
            view.Show(); //a Show() metódus létrehoz egy ablakot a view-ból, aminek a kontextusa a korábban definiált nézetmodell
        }

        // Step 4 / b
        private void ViewModel_ChangeColor(object sender, EventArgs e)
        {
            viewModel.ChangeColor();
        }

        private void ViewModel_Quit(object sender, EventArgs e)
        {
            view.Close();
        }
    }
}
