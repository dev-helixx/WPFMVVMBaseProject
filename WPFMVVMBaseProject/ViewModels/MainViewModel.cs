using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFMVVMBaseProject.Commands;
using WPFMVVMBaseProject.Models;

namespace WPFMVVMBaseProject.ViewModels
{

  

  class MainViewModel : BaseViewModel
  {

    #region Fields
    private TestModel model = new TestModel();
    #endregion


    #region Events
    public event PubSubEventHandler<object> PubSubCheckedHandler;
    #endregion

    #region Commands
    public ActionCommand LoadButtonCommand { get; set; }

    #endregion

    public MainViewModel()
    {
      
      model.Value = ConfigurationManager.AppSettings["TestKey"];

      // Publish an event with eventName = PubSubTest. Others can subscribe to said eventName, in order to catch when it is raised
      // Use: PubSub<object>.Subscribe("PubSubTest", PubSubTestHandler); to subscribe to the event in another class
      PubSub<object>.PublishEvent("PubSubTest", PubSubCheckedHandler);


      // Initialize other viewmodels
      //WatchedMoviesViewModel = new WatchedMoviesViewModel(readingModel.WatchedMovies); //// Pass list of movie objects to the WatchedMovies
      //NonWatchedMoviesViewModel = new NonWatchedMoviesViewModel(readingModel.NonWatchedMovies); // Pass list of movie objects to the NonWatchedMovies
      //AddMovieViewModel = new AddMovieViewModel(this, WatchedMoviesViewModel, NonWatchedMoviesViewModel); // Pass reference for mainviewmodel and both datagrids, so when we add a new movie we know where to put it

      // Subscribe to the ViewModels' OnpropertyChanged event to look for changes
      //WatchedMoviesViewModel.PropertyChanged += MoviesViewModel_PropertyChanged;
      //NonWatchedMoviesViewModel.PropertyChanged += MoviesViewModel_PropertyChanged;
      //this.PropertyChanged += MainWindowViewModel_PropertyChanged;    // Subscribe to MainViewModels OnPropertyChanged event to check for changes in MainViewModel



      // Register commands so we are able to execute specific buttons
      RegisterCommand(LoadButtonCommand = new ActionCommand(Load, CanLoad));


      // Set load button to active
      CheckCanLoad = true;

    }


    #region Public Properties
    // Controls whether Load button is enabled or not
    private bool _checkCanLoad;
    public bool CheckCanLoad
    {
      get { return _checkCanLoad; }
      set
      {
        if (value != _checkCanLoad)
        {
          _checkCanLoad = value;
          OnPropertyChanged(nameof(CheckCanLoad));
        }
      }
    }


    private string _textBoxInMainWindow;
    public string TextBoxInMainWindow
    {
      get { return _textBoxInMainWindow; }
      set
      {
        if (value != _textBoxInMainWindow)
        {
          _textBoxInMainWindow = value;
          OnPropertyChanged(nameof(TextBoxInMainWindow));
        }
      }
    }

    #endregion

    private void Load()
    {
      TextBoxInMainWindow = model.Value;
    }


    private bool CanLoad()
    {
      // We always want to be able to click the button.
      return CheckCanLoad;
    }




 
  }
}
