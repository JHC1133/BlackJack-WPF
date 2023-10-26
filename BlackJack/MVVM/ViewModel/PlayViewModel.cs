using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackJack.MVVM.ViewModel
{
    public class PlayViewModel : ObservableObject
    {


        GameManager _gameManager;

        public ICommand HitCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayViewModel()
        {
            _gameManager = GameManager.Instance;
            HitCommand = new RelayCommand(player => ExecuteHit(player));
        }


        public GameManager GameManager { get => _gameManager; }

        private void ExecuteHit(object player)
        {
            if (player is Player currentPlayer)
            {
                _gameManager.Hit(currentPlayer);

                OnPropertyChanged(nameof(currentPlayer));

                Debug.WriteLine("Player HitCommand executed");
            }
        }
    }
}
