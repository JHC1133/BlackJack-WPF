using BlackJack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand PlayViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public PlayViewModel PlayVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyCanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            PlayVM = new PlayViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            PlayViewCommand = new RelayCommand(o =>
            {
                CurrentView = PlayVM;
            });
        }

    }
}
