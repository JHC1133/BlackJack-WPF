using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlackJack.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region MVVM
        public RelayCommand OptionsViewCommand { get; set; }
        public RelayCommand PlayViewCommand { get; set; }

        public OptionsViewModel OptionsVM { get; set; }
        public PlayViewModel PlayVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public MainViewModel()
        {

            OptionsVM = new OptionsViewModel();
            PlayVM = new PlayViewModel();
            CurrentView = OptionsVM; // Setting the default view
           
            OptionsViewCommand = new RelayCommand(o =>
            {
                CurrentView = OptionsVM;
            });

            PlayViewCommand = new RelayCommand(o =>
            {
                CurrentView = PlayVM;
            });
        }

    }
}
