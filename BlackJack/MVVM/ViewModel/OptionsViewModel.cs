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
        Helper helper;
        GameManager _gameManager = GameManager.Instance;

        private string _selectedPlayer;

        private int _numberOfPlayers = 4;
        private int _numberOfDecks = 4;

        private int _minNumberOfPlayers = 2;
        private int _maxNumberOfPlayers = 4;
        private int _minNumberOfDecks = 1;
        private int _maxNumberOfDecks = 4;

        private bool _optionsSaved;


        /// <summary>
        /// Holds an Execute and CanExecute method, which executes respectively checks if the execution is possible. In this case will be used for saving data on the OptionsView
        /// </summary>
        public ICommand SaveCommand { get; }
        public ICommand RemoveFromTableCommand { get; }
        public bool OptionsSaved { get => _optionsSaved; set => _optionsSaved = value; }
        public GameManager GameManager { get => _gameManager; set => _gameManager = value; }
        public int NumberOfPlayers { get => _numberOfPlayers; set => _numberOfPlayers = value; }
        public int NumberOfDecks { get => _numberOfDecks; set => _numberOfDecks = value; }
        public string SelectedPlayer { get => _selectedPlayer; set => _selectedPlayer = value; }

        public OptionsViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            RemoveFromTableCommand = new RelayCommand(o => ExecuteRemove());
            helper = new Helper();
        }

        private void ExecuteRemove()
        {
            if (SelectedPlayer != null)
            {
                _gameManager.RemoveFromTable(SelectedPlayer);
            }
        }

        private bool CanExecuteSave(object parameter)
        {
            //returns true if
            return OptionsValueCheck();
        }

        private void ExecuteSave(object parameter)
        {
            if (CanExecuteSave(parameter))
            {
                GameManager.InitilizeGame(NumberOfDecks, NumberOfPlayers);

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
            bool correctAmountPlayers = helper.WithinMinMaxValueCheck(_minNumberOfPlayers, _maxNumberOfPlayers, NumberOfPlayers);
            bool correctAmountDecks = helper.WithinMinMaxValueCheck(_minNumberOfDecks, _maxNumberOfDecks, NumberOfDecks);

            if (correctAmountPlayers && correctAmountDecks)
            {
                Debug.WriteLine("OptionsValueCheck = true");
                return true;
            }
            return false;
        }


   
    }
}
