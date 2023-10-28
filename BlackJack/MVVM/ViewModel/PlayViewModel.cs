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

        GameManager _gameManager;
     
        public ICommand HitCommand { get; private set; }
        public ICommand StandCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public PlayViewModel()
        {
            _gameManager = GameManager.Instance;

            HitCommand = new RelayCommand(player => ExecuteHit(player));
            StandCommand = new RelayCommand(player => ExecuteStand(player));

            _gameManager.BlackJackEvent += HandleBlackJackEvent;
            _gameManager.PlayerBustEvent += HandlePlayerBustEvent;
            _gameManager.DealerBustEvent += HandleDealerBustEvent;
        }

        public GameManager GameManager { get => _gameManager; }

        private void ExecuteHit(object player)
        {
            if (player is Player currentPlayer)
            {
                _gameManager.Hit(currentPlayer);

                OnPropertyChanged(nameof(currentPlayer));
                OnPropertyChanged(nameof(_gameManager));

                Debug.WriteLine("Player HitCommand executed");
            }
        }

        private void ExecuteStand(object player)
        {
            if (player is Player currentPlayer)
            {
                _gameManager.Stand(currentPlayer);
            }
        }

        private void ExecuteRefresh(object parameter)
        {
            OnPropertyChanged(nameof(_gameManager));
        }

        private void HandleBlackJackEvent(object sender, Func<Player> getPlayerFunc)
        {
            Player playerWithBlackJack = getPlayerFunc();

            if (playerWithBlackJack != null)
            {
                //MessageBox.Show($"{playerWithBlackJack.Name} got a Blackjack!");
                //playerWithBlackJack
            }
        }

        private void HandlePlayerBustEvent(object sender, Func<Player> getPlayerFunc)
        {
            Player playerWithBlackJack = getPlayerFunc();

            if (playerWithBlackJack != null)
            {
                MessageBox.Show($"{playerWithBlackJack.Name} got bust!");
            }
        }

        private void HandleDealerBustEvent(object sender, EventArgs e)
        {
            MessageBox.Show($"{_gameManager.Dealer.Name} got bust");
        }

    }
}
