using BlackJack.Core;
using EL;
using GameCardLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.MVVM.ViewModel
{
    public class StatsViewModel : ObservableObject
    {
        GameManager _gameManager;
        private ObservableCollection<Game> games;
        private ObservableCollection<DealerStatistics> dealer;
        private ObservableCollection<PlayerStatistics> players;

        private ObservableCollection<GamePlayerStatisticsIntermediary> selectedGamePlayerStatisticsIntermediary;

        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set
            {
                selectedGame = value;
                OnPropertyChanged(nameof(SelectedGame));
            }
        }

        public ObservableCollection<Game> Games
        {
            get { return games; }
            set
            {
                games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        public ObservableCollection<DealerStatistics> Dealer
        {
            get { return dealer; }
            set
            {
                dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public ObservableCollection<PlayerStatistics> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        public ObservableCollection<GamePlayerStatisticsIntermediary> SelectedGamePlayerStatisticsIntermediary
        {
            get { return selectedGamePlayerStatisticsIntermediary; }
            set
            {
                selectedGamePlayerStatisticsIntermediary = value;
                OnPropertyChanged(nameof(SelectedGamePlayerStatisticsIntermediary));
            }
        }

        public StatsViewModel()
        {
            _gameManager = GameManager.Instance;

            Games = new ObservableCollection<Game>(_gameManager.GetGames());
            Dealer = new ObservableCollection<DealerStatistics>(_gameManager.GetDealer());
            Players = new ObservableCollection<PlayerStatistics>(_gameManager.GetPlayers());
        }

    }
}
