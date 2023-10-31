using EL;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.IdentityModel.Tokens;

namespace GameCardLibrary
{
    public class GameManager : INotifyPropertyChanged
    {
        RulesCheck check;
        Dealer _dealer;
        Deck _deck;
        Random rand;

        List<string> _fixedNameList;
        List<string> _nameList;
        List<Player> _players;
        List<Deck> _decks;
        ObservableCollection<Player> _observablePlayers;

        private int _numberOfPlayers;
        private int _numberOfDecks;
        private bool _nextRoundBtnVisible;

        // Lambda statement
        private string PlayerName => RandomizeNameFunc(NameList);

        private Func<List<string>, string> RandomizeNameFunc = (nameList) =>
        {
            if (nameList.Count == 0)
            {
                return string.Empty;
            }

            Random rand = new Random();
            int i = rand.Next(0, nameList.Count);
            string s = nameList[i];
            nameList.RemoveAt(i);
            return s;
        };

        #region Events
        //public event EventHandler<EventArgs> DealerHitEvent;
        public event EventHandler<Func<Player>> PlayerBustEvent;
        public event EventHandler<Func<Player>> BlackJackEvent;
        public event EventHandler<EventArgs> DealerBustEvent;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public bool NextRoundBtnVisible
        {
            get { return _nextRoundBtnVisible; }
            set
            {
                if (_nextRoundBtnVisible != value)
                {
                    _nextRoundBtnVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Singleton
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }


        #endregion

        private static GameManager _instance;
        public int NumberOfPlayers { get => _numberOfPlayers; }
        public int NumberOfDecks { get => _numberOfDecks; }
        public Dealer Dealer { get => _dealer; set => _dealer = value; }
        public  ObservableCollection<Player> ObservablePlayers { get => _observablePlayers; set => _observablePlayers = value; }
        public List<string> NameList { get => _nameList; set => _nameList = value; }
        public List<string> FixedNameList { get => _fixedNameList; set => _fixedNameList = value; }

        private GameManager()
        {
            rand = new Random();
            check = new RulesCheck();
            InitializePlayerNames();

        }

        #region Setters and Initializers



        public void InitilizeGame(int numberOfDecks, int numberOfPlayers)
        {
            SetNumberOfPlayers(numberOfPlayers);
            SetNumberOfDecks(numberOfDecks);
            InitializeGameDeck(numberOfDecks);
            InitializePlayers(numberOfPlayers);
            InitializeDealer();
            InitializeObservableList();
            //GameConditionsCheck();

            CreateNewGame();
        }

        /// <summary>
        /// Copies _players (List) to _observablePlayers (ObservableCollection)
        /// </summary>
        private void InitializeObservableList()
        {
            //Move to helper as a more generic method

            _observablePlayers = new ObservableCollection<Player>();

            foreach (Player player in _players)
            {
                _observablePlayers.Add(player);
            }
        }

        private void SetNumberOfPlayers(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
            Debug.WriteLine("NumberOfPlayers: " + _numberOfPlayers);
        }

        private void SetNumberOfDecks(int numberOfDecks)
        {
            _numberOfDecks = numberOfDecks;
            Debug.WriteLine("NumberOfDecks: " + _numberOfDecks);
        }

        /// <summary>
        /// Initializes the name array for players
        /// </summary>
        private void InitializePlayerNames()
        {
            NameList = new List<string>
            {
                    "Joar",
                    "Farid",
                    "Wilmer",
                    "Simon",
                    "Marco",
                    "Brandon",
                    "Kristoffer",
                    "Dick",
                    "Jose",
                    "Kevin",
                    "Sofia",
                    "Gorm",
                    "Zeke",
                    "McThundertits"
            };

            FixedNameList = new List<string>
            {
                    "Joar",
                    "Farid",
                    "Wilmer",
                    "Simon",
                    "Marco",
                    "Brandon",
                    "Kristoffer",
                    "Dick",
                    "Jose",
                    "Kevin",
                    "Sofia",
                    "Gorm",
                    "Zeke",
                    "McThundertits",
                    "Croupier"
            };
        }

        /// <summary>
        /// Returns a random name from the name array for players and removes it from the list
        /// </summary>
        /// <returns></returns>
        private string RandomizeName()
        {
            Random rand = new Random();
            int i = rand.Next(0, NameList.Count - 1);
            string s = NameList[i];
            NameList.RemoveAt(i);
            return s;
        }

        

        /// <summary>
        /// Intializes the players and gives them two cards. Needs to be initialized after InitiliazeGameDeck()
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void InitializePlayers(int numberOfPlayers)
        {           
            _players = new List<Player>(numberOfPlayers);

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                int randomDeckValue = rand.Next(0, _numberOfDecks - 1);
                Hand playerHand = new Hand();

                // Draws two cards from the deck and adds them to the player's hand
                playerHand.AddCard(_decks[randomDeckValue].DrawCard());
                playerHand.AddCard(_decks[randomDeckValue].DrawCard());

                Player player = new Player(playerHand, PlayerName);
                _players.Add(player);


                //Debugging
                Debug.WriteLine($"Game is created with Player {_players[i].Name} using cards with value {_players[i].Hand.Score} on hand ");
                Debug.WriteLine("Current cards: " + _players[i].Hand.ToString());
            }

            Debug.WriteLine("Players initialized with: " + _numberOfPlayers);
            
        }

        /// <summary>
        /// Initializes the dealer with two cards
        /// </summary>
        private void InitializeDealer()
        {
            int randomDeckValue = rand.Next(0, NumberOfDecks - 1);
            Hand dealerHand = new Hand();

            dealerHand.AddCard(_decks[randomDeckValue].DrawCard());
            dealerHand.AddCard(_decks[randomDeckValue].DrawCard());
            Dealer = new Dealer(dealerHand);

            //Debugging
            Debug.WriteLine($"Game is created with Player {Dealer.Name} using cards with value {Dealer.Hand.Score} on hand ");
            Debug.WriteLine("Current cards: " + Dealer.Hand.ToString());
        }

        /// <summary>
        /// Needs to be initialized before InitializePlayers()
        /// </summary>
        /// <param name="numberOfDecks"></param>
        private void InitializeGameDeck(int numberOfDecks)
        {
            _decks = new List<Deck>(numberOfDecks);

            for (int i = 0; i < _numberOfDecks; i++)
            {
                Deck deck;
                _decks.Add(deck = new Deck(CreateStandardDeck()));

                deck.Shuffle();

                //Debugging
                //Debug.WriteLine($"Deck content: {_decks[i].ToString()}");
            }

            
        }

        /// <summary>
        /// Creates a standard deck of 52 cards
        /// </summary>
        /// <returns></returns>
        private List<Card> CreateStandardDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    Card card;
                    deck.Add(card = new Card(suit, value));

                    //Debug.WriteLine(card.ToString());
                }
            }

            
            return deck;
        }

        #endregion

        #region GameMechanics

        /// <summary>
        /// Gives the player a card if CanHit(Player player) is true
        /// </summary>
        /// <param name="player"></param>
        public void Hit(Player player)
        {
            int randomDeckValue = rand.Next(0, _numberOfDecks - 1);

            foreach (Player playerInList in _players)
            {
                if (player == playerInList)
                {
                    if (check.CanHit(playerInList))
                    {
                        AddCardToPlayerHand(playerInList, 1);
                    }                  
                }
            }           
        }

        /// <summary>
        /// Gives the dealer a card if CanHit(Dealer dealer) is true
        /// </summary>
        /// <param name="dealer"></param>
        public void Hit(Dealer dealer)
        {
            int randomDeckValue = rand.Next(0, _numberOfDecks - 1);

            if (check.CanHit(dealer))
            {
                AddCardToDealerHand(_dealer, 1);
            }
        }

        /// <summary>
        /// Player stands and IsFinished is set to true
        /// </summary>
        /// <param name="player"></param>
        public void Stand(Player player)
        {
            foreach (Player playerInList in _players)
            {
                if (player == playerInList)
                {
                    playerInList.IsFinished = true;
                    FinishedRoundCheck();
                }
            }
        }

        /// <summary>
        /// Executes a new round. Resets hands, deal cards and checks for Blackjack
        /// </summary>
        public void NewRound()
        {
            ResetPlayers();
            ClearHands();
            DealCards();
            BlackJackCheck();
        }

        public void UpdateStatistics()
        {
            UpdatePlayerStatistics();
            UpdateDealerStatistics();
        }

        /// <summary>
        /// Resets the properties of the players
        /// </summary>
        private void ResetPlayers()
        {
            foreach (Player player in _players)
            {
                player.IsFinished = false;
                player.IsBust = false;
                player.Winner = false;
                player.StateText = "";
            }
        }

        /// <summary>
        /// Clear hands of the players and dealer
        /// </summary>
        private void ClearHands()
        {
            _dealer.Hand.Clear();

            foreach (Player player in _players)
            {
                player.Hand.Clear();
            }
        }

        /// <summary>
        /// Deals cards to the players and the dealer
        /// </summary>
        private void DealCards()
        {
            foreach (Player player in _players)
            {
                AddCardToPlayerHand(player, 2);
            }

            AddCardToDealerHand(_dealer, 2);
        }

        /// <summary>
        /// Adds card to player hand. Number of cards can be of value 1 or 2 only
        /// </summary>
        /// <param name="player"></param>
        /// <param name="numberOfCards"></param>
        private void AddCardToPlayerHand(Player player, int numberOfCards)
        {
            int randomDeckValue = rand.Next(0, _numberOfDecks - 1);

            if (numberOfCards == 1)
            {
                player.Hand.AddCard(_decks[randomDeckValue].DrawCard());
            }
            else if (numberOfCards == 2)
            {
                player.Hand.AddCard(_decks[randomDeckValue].DrawCard());
                player.Hand.AddCard(_decks[randomDeckValue].DrawCard());
            }
            else
            {
                Debug.WriteLine("Player can only be given 1 or 2 cards at the same time");
            }
        }

        /// <summary>
        /// Adds card to dealer hand. Number of cards can be of value 1 or 2 only
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="numberOfCards"></param>
        private void AddCardToDealerHand(Dealer dealer, int numberOfCards)
        {
            int randomDeckValue = rand.Next(0, _numberOfDecks - 1);

            if (numberOfCards == 1)
            {
                dealer.Hand.AddCard(_decks[randomDeckValue].DrawCard());
            }
            else if (numberOfCards == 2)
            {
                dealer.Hand.AddCard(_decks[randomDeckValue].DrawCard());
                dealer.Hand.AddCard(_decks[randomDeckValue].DrawCard());
            }
            else
            {
                Debug.WriteLine("Dealer can only be given 1 or 2 cards at the same time");
            }
        }

        /// <summary>
        /// Checks if all players have finished, if true Dealer will continue
        /// </summary>
        /// <returns></returns>
        private void FinishedRoundCheck()
        {
            bool allPlayersFinished = true;

            foreach (Player player in _players)
            {
                if (!player.IsFinished)
                {
                    allPlayersFinished = false;
                    NextRoundBtnVisible = false;
                }
            }

            if (allPlayersFinished)
            {
                while (check.CanHit(_dealer))
                {
                    Hit(_dealer);
                }
                GameConditionsCheck();
                NextRoundBtnVisible = true;
            } 
        }

        /// <summary>
        /// Checks if player has blackjack
        /// </summary>
        public void BlackJackCheck()
        {
            if (_players != null)
            {
                foreach (Player player in _players)
                {
                    if (check.PlayerWonWithBlackjack(player, _dealer))
                    {
                        BlackJackEvent?.Invoke(this, () => player);
                        player.StateText = State.Blackjack.ToString();
                        player.Blackjacks++;
                        //Stand(player);
                        Debug.WriteLine("BlackJackEvent sent");
                    }
                }
            }
           
        }

        /// <summary>
        /// Checks win and lose conditions
        /// </summary>
        public void GameConditionsCheck()
        {

            foreach (Player player in _players)
            {
                if (check.IsBust(player.Hand))
                {
                    PlayerBustEvent?.Invoke(this, () => player);
                    player.StateText = State.Bust.ToString();
                    player.Busts++;
                    player.Losses++;
                    //Stand(player);
                    Debug.WriteLine("PlayerBustEvent sent");
                }
                else if (check.IsTie(player, _dealer))
                {
                    player.StateText = State.Tie.ToString();
                    player.Ties++;
                    _dealer.Ties++;
                }
                else if (check.PlayerWon(player, _dealer) && !check.IsBust(player.Hand))
                {
                    player.StateText = State.Winner.ToString();
                    player.Wins++;
                    _dealer.Losses++;
                }
                else if (check.PlayerWon(player, _dealer) && !check.IsBust(player.Hand) && check.IsBust(_dealer.Hand))
                {
                    player.StateText = State.Winner.ToString();
                    player.Wins++;
                    _dealer.Losses++;
                }
                else if (check.DealerWon(player, _dealer) && !check.IsBust(_dealer.Hand))
                {
                    player.StateText = State.Loser.ToString();
                    player.Losses++;
                    _dealer.Wins++;
                }
                else if (check.IsBust(_dealer.Hand))
                {
                    DealerBustEvent?.Invoke(this, EventArgs.Empty);
                    player.StateText = State.Winner.ToString();
                    _dealer.Busts++;
                    _dealer.Losses++;
                }

                Debug.WriteLine(player.StateText);
            }


        }

        #endregion


        #region DALCommunications

        public void CreateNewGame()
        {
            UpdatePlayerStatistics();
            UpdateDealerStatistics();

            List<string> playerNames = _players.Select(player => player.Name).ToList();
            Handler DALhandler = new Handler();
            DALhandler.CreateNewGame(playerNames, _dealer.Name);
            
        }

        /// <summary>
        /// Gets the current playerStatistics from the DAL and updates them with the current stats
        /// </summary>
        private void UpdatePlayerStatistics()
        {
            Handler DALhandler = new Handler();
            List<PlayerStatistics> updatedPlayerStats = new List<PlayerStatistics>();

            foreach (Player player in _players)
            {
                string playerName = player.Name;

                PlayerStatistics playerStats = DALhandler.GetPlayerStatistics(playerName);

                if (playerStats == null)
                {
                    // Add
                    playerStats = new PlayerStatistics
                    {
                        PlayerName = playerName,
                        Wins = player.Wins,
                        Busts = player.Busts,
                        Ties = player.Ties,
                        Losses = player.Losses,
                        Blackjacks = player.Blackjacks
                    };
                }
                else
                {
                    // Change
                    playerStats.Wins += player.Wins;
                    playerStats.Busts += player.Busts;
                    playerStats.Ties += player.Ties;
                    playerStats.Losses += player.Losses;
                    playerStats.Blackjacks += player.Blackjacks;
                }

                //DALhandler.UpdatePlayerStatistics(playerStats);
                updatedPlayerStats.Add(playerStats);
            }

            foreach (PlayerStatistics playerStats in updatedPlayerStats)
            {
                DALhandler.UpdatePlayerStatistics(playerStats);
            }

            DALhandler.PrintPlayerTableContent();
        }

        /// <summary>
        /// Gets the current dealerStatistics from the DAL and updates them with the current stats
        /// </summary>
        private void UpdateDealerStatistics()
        {
            Handler DALhandler = new Handler();

            DealerStatistics dealerStats = DALhandler.GetDealerStatistics();

            if (dealerStats == null)
            {
                dealerStats = new DealerStatistics
                {
                    Name = _dealer.Name,
                    Wins = _dealer.Wins,
                    Busts = _dealer.Busts,
                    Losses = _dealer.Losses,
                    Blackjacks = _dealer.Blackjacks
                };
            }
            else
            {
                dealerStats.Wins += _dealer.Wins;
                dealerStats.Busts += _dealer.Busts;
                dealerStats.Losses += _dealer.Losses;
                dealerStats.Ties += _dealer.Ties;
                dealerStats.Blackjacks += _dealer.Blackjacks;
            }
  

            DALhandler.UpdateDealerStatistics(dealerStats);
        }

        public void RemoveFromTable(string playerName)
        {
            Handler DALhandler = new Handler();

            DALhandler.RemoveItemFromTable(playerName);

            Debug.WriteLine("GameManager.RemoveFromTable() called");
        }

        #endregion

    }
}
