using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class GameManager
    {
        RulesCheck check;
        Dealer _dealer;
        Deck _deck;
        Random rand;

        List<string> _nameList;
        List<Player> _players;
        List<Deck> _decks;
        ObservableCollection<Player> _observablePlayers;

        private int _numberOfPlayers;
        private int _numberOfDecks;

        //public event EventHandler<EventArgs> DealerHitEvent;
        public event EventHandler<Func<Player>> PlayerBustEvent;
        public event EventHandler<Func<Player>> BlackJackEvent;
        public event EventHandler<EventArgs> DealerBustEvent;


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

        private GameManager()
        {
            rand = new Random();
            check = new RulesCheck();
        }

        #region Setters and Initializers
        public void InitilizeGame(int numberOfDecks, int numberOfPlayers)
        {
            SetNumberOfPlayers(numberOfPlayers);
            SetNumberOfDecks(numberOfDecks);
            InitializeGameDeck(numberOfDecks);
            InitializePlayerNames();
            InitializePlayers(numberOfPlayers);
            InitializeDealer();
            InitializeObservableList();

            //GameConditionsCheck();
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
            _nameList = new List<string>
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
        }

        /// <summary>
        /// Returns a random name from the name array for players and removes it from the list
        /// </summary>
        /// <returns></returns>
        private string RandomizeName()
        {
            Random rand = new Random();
            int i = rand.Next(0, _nameList.Count - 1);
            string s = _nameList[i];
            _nameList.RemoveAt(i);
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

                Player player = new Player(playerHand, RandomizeName());
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

                    Debug.WriteLine(card.ToString());
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

        private void NewRound()
        {
            foreach (Player player in _players)
            {
                player.IsFinished = false;
                player.IsBust = false;
                player.Winner = false;
                player.StateText = "";
            }

            ClearHands();
            DealCards();
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
                }
            }

            if (allPlayersFinished)
            {
                while (check.CanHit(_dealer))
                {
                    Hit(_dealer);
                }
                GameConditionsCheck();
                NewRound();
            }
        }

        /// <summary>
        /// Checks win and lose conditions
        /// </summary>
        public void GameConditionsCheck()
        {
            
            foreach (Player player in _players)
            {
                if (check.PlayerWonWithBlackjack(player, _dealer))
                {
                    BlackJackEvent?.Invoke(this, () => player);
                    player.StateText = "Blackjack";
                    Debug.WriteLine("BlackJackEvent sent");
                }
                else if (check.IsBust(player.Hand))
                {
                    PlayerBustEvent?.Invoke(this, () => player);
                    player.StateText = "Bust";
                    Debug.WriteLine("PlayerBustEvent sent");
                }
            }

            if (check.IsBust(_dealer.Hand))
            {
                DealerBustEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

    }
}
