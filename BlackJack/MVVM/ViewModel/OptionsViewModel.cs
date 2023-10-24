using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackJack.MVVM.ViewModel
{
    class OptionsViewModel : ObservableObject
    {
        private int _numberOfPlayers;
        private int _numberOfDecks;

        public int NumberOfPlayers
        {
            get
            {
                return _numberOfPlayers;
            }
            set
            {
                _numberOfPlayers = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfDecks
        {
            get
            {
                return _numberOfDecks;
            }
            set
            {
                _numberOfDecks = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Holds an Execute and CanExecute method, which executes respectively checks if the execution is possible. In this case will be used for saving data on the OptionsView
        /// </summary>
        public ICommand SaveCommand { get; }

        public OptionsViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object parameter)
        {
            return true;
        }

        private void ExecuteSave(object parameter)
        {

        }
    }
}
