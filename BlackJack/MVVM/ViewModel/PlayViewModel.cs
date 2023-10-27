using BlackJack.Core;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BlackJack.MVVM.ViewModel
{
    public class PlayViewModel : ObservableObject
    {
        private int _score;

        GameManager _gameManager;
     
        public ICommand HitCommand { get; private set; }
        public ICommand StandCommand { get; private set; }

        public PlayViewModel()
        {
            _gameManager = GameManager.Instance;

            HitCommand = new RelayCommand(player => ExecuteHit(player));
            StandCommand = new RelayCommand(player => ExecuteStand(player));

            _gameManager.BlackJackEvent += HandleBlackJackEvent;
        }

        public GameManager GameManager { get => _gameManager; }

        public int VMScore
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ExecuteHit(object player)
        {
            if (player is Player currentPlayer)
            {
                _gameManager.Hit(currentPlayer);
                _score = currentPlayer.Hand.Score;

                OnPropertyChanged(nameof(currentPlayer));

                Debug.WriteLine("Player HitCommand executed");
                Debug.WriteLine(_score);
            }
        }

        private void ExecuteStand(object player)
        {
            if (player is Player currentPlayer)
            {
                _gameManager.Stand(currentPlayer);
            }
        }

        private void HandleBlackJackEvent(object sender, Func<Player> getPlayerFunc)
        {
            Player playerWithBlackJack = getPlayerFunc();

            if (playerWithBlackJack != null)
            {
                MessageBox.Show($"{playerWithBlackJack.Name} got a Blackjack!");
            }
        }

    }
}
