using System.Configuration;
using System.Windows;
using WPFMVVMBaseProject.ViewModels;

namespace WPFMVVMBaseProject
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      //Set datacontext of main window
      var mainViewModel = new MainViewModel();
      MainWindow window = new MainWindow { DataContext = mainViewModel };
      window.Show();


    }

  }
}
