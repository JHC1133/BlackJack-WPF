using System;
using System.Collections.Generic;
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

        List<Player> players;

        int _numberOfPlayers;

        public GameManager(int argNumberOfPlayers, int argNumberOfDecks)
        {

            List<Card> standardDeck = CreateStandardDeck();

            //dealer = new Dealer();
            deck = new Deck(standardDeck);
        }

        private void InitializePlayers(int numberOfPlayers)
        {
            players = new List<Player>(numberOfPlayers);
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
