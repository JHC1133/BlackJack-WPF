using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    internal class Hand
    {
        private List<Card> _cards;

        Deck _deck;

        public Card LastCard { get; }
        public int NumberOfCards { get; }
        public int Score { get; }

        public Hand(Deck deck)
        {
            _deck = deck;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        private void Clear()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
