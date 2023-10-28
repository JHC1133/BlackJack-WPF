using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace GameCardLibrary
{
    public class Deck
    {

        private List<Card> _cards;

        Random randomArranger;

        public event EventHandler<EventArgs> EmptyDeckEvent;

        public bool GameIsDone { get; set; }

        public Deck(List<Card> cardList)
        {
            _cards = cardList;
        }

        /// <summary>
        /// Draws a card from the decks availabe, as well as removing drawn card from the deck
        /// </summary>
        /// <returns></returns>
        public Card DrawCard()
        {
            if (_cards.Count > 0)
            {
                Card drawnCard = _cards[0];
                _cards.RemoveAt(0);
                return drawnCard;
            }
            else
            {
                Debug.WriteLine("The deck is empty, creating new");
                _cards = CreateStandardDeck();
                return DrawCard();
            }
        }

        public List<Card> GetTwoCards()
        {
            if (_cards.Count < 2)
            {
                throw new InvalidOperationException("There's not enough cards in the deck");
            }

            List<Card> twoCards = new List<Card> { DrawCard(), DrawCard() };
            return twoCards;
        }

        public void DiscardCards()
        {
            _cards.Clear();
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

                    //Debug.WriteLine(card.ToString());
                }
            }


            return deck;
        }

        /// <summary>
        /// Using a Fisher-Yates style shuffle
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();

            int n = _cards.Count;

            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card temp = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = temp;
            }
        }

        private int NumberOfCards()
        {
            return _cards.Count;
        }

        

        private void InitializeDeck(List<Card> cardList)
        {

        }

        private void OnShuffle(object source, EventArgs e)
        {

        }

        private void OnEmptyDeck(object source, EventArgs e)
        {
            EmptyDeckEvent?.Invoke(this, e);
        }


    }
}
