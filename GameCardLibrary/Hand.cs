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
        public int Score => CalculateScore();

        private int CalculateScore()
        {
            int score = 0;

            foreach (Card card in _cards)
            {
                score += card.GetCardValue();
            }
            return score;
        }

        public Hand()
        {
            _cards = new List<Card>();
        }

        public int CalculateHandValue()
        {
            int value = 0;
            int numberOfAces = 0;

            foreach (Card card in _cards)
            {
                value += card.GetCardValue();

                if (card.Value == Value.Ace)
                {
                    numberOfAces++;
                }
            }

            if (numberOfAces > 0 && value > 21)
            {
                value -= 10;
                numberOfAces--;
            }

            return value;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public List<Card> GetCurrentCards()
        {
            return _cards;
        }

        private void Clear()
        {
            _cards.Clear();
        }

        public string PrintCurrentCards()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Card card in _cards)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
