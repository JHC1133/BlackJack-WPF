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
        Dealer dealer;
        Deck deck;

        List<Player> _players;
        List<Deck> _decks;

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

        private GameManager()
        {

            List<Card> standardDeck = CreateStandardDeck();

            //dealer = new Dealer();
            deck = new Deck(standardDeck);
        }

        public void InitilizeGame(int numberOfDecks, int numberOfPlayers)
        {
            SetNumberOfPlayers(numberOfPlayers);
            SetNumberOfDecks(numberOfDecks);
            InitializeGameDeck(numberOfDecks);
            InitializePlayers(numberOfPlayers);
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

                Hand playerHand = new Hand(_decks[i]);
                _players.Add(new Player(playerHand));

                Debug.WriteLine($"Game is created with Player {_players[i].Name} using cards {_players[i].Hand.NumberOfCards} on hand ");
            }

            Debug.WriteLine("Players initialized with: " + _numberOfPlayers);
            
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
                _decks.Add(new Deck(CreateStandardDeck()));

                Debug.WriteLine($"Deck content: {_decks[i].ToString()}");
            }
        }

        private List<Card> CreateStandardDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    deck.Add(new Card(suit, value));
                }
            }

            return deck;
        }
    }
}
