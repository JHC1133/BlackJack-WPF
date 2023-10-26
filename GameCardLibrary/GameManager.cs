using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class GameManager
    {
        RulesCheck check;
        Dealer _dealer;
        Deck _deck;

        List<Player> _players;
        List<Deck> _decks;

        Random rand;

        private int _numberOfPlayers;
        private int _numberOfDecks;

        private static GameManager _instance;

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

        public int NumberOfPlayers { get => _numberOfPlayers; }
        public int NumberOfDecks { get => _numberOfDecks; }
        public Dealer Dealer { get => _dealer; set => _dealer = value; }

        private GameManager()
        {
            rand = new Random();
        }

        public void InitilizeGame(int numberOfDecks, int numberOfPlayers)
        {
            SetNumberOfPlayers(numberOfPlayers);
            SetNumberOfDecks(numberOfDecks);
            InitializeGameDeck(numberOfDecks);
            InitializePlayers(numberOfPlayers);
            InitializeDealer();
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
        /// Needs to be initialized after InitiliazeGameDeck()
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void InitializePlayers(int numberOfPlayers)
        {           
            _players = new List<Player>(numberOfPlayers);

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                int randomDeckValue = rand.Next(0, 3);
                Hand playerHand = new Hand();

                // Draws two cards from the deck and adds them to the player's hand
                playerHand.AddCard(_decks[randomDeckValue].DrawCard());
                playerHand.AddCard(_decks[randomDeckValue].DrawCard());

                Player player = new Player(playerHand);
                _players.Add(player);


                //Debugging
                Debug.WriteLine($"Game is created with Player {_players[i].Name} using cards with value {_players[i].Hand.Score} on hand ");
                Debug.WriteLine("Current cards: " + _players[i].Hand.ToString());
            }

            Debug.WriteLine("Players initialized with: " + _numberOfPlayers);
            
        }

        private void InitializeDealer()
        {
            int randomDeckValue = rand.Next(0, 3);
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
    }
}
