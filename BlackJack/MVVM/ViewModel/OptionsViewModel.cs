using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UtilitiesLib;

namespace BlackJack.MVVM.ViewModel
{
    class OptionsViewModel : ObservableObject
    {
        GameManager _gameManager = GameManager.Instance;
        private int _numberOfPlayers;
        private int _numberOfDecks;
        private bool _optionsSaved;

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
        public bool OptionsSaved { get => _optionsSaved; set => _optionsSaved = value; }

        public OptionsViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }


        public bool CanExecuteSave(object parameter)
        {
            //returns true if
            return OptionsValueCheck();
        }

        private void ExecuteSave(object parameter)
        {
            if (CanExecuteSave(parameter))
            {
                _gameManager.InitilizeGame(_numberOfDecks, _numberOfPlayers);

                OptionsSaved = true;
            }
            else
            {
                MessageBox.Show("Please use valid integers as input");
            }
        }

        /// <summary>
        /// Returns true if the values asked for in OptionsView are within range of the program rules
        /// </summary>
        private bool OptionsValueCheck()
        {
            if (_numberOfPlayers >= 1 && _numberOfPlayers <= 3 &&
                _numberOfDecks >= 1 && _numberOfDecks <= 4)
            {
                Debug.WriteLine("OptionsValueCheck = true");
                return true;
            }
            return false;
        }


   
    }
}
