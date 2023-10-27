using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class Hand
    {
        private int _numberOfCards;
        private List<Card> _cards;

        ObservableCollection<Card> _observableHandCollection;
        public Card LastCard { get; }
        public int NumberOfCards { get => _numberOfCards; }
        public int Score => CalculateHandValue(); // Lambda?

        public ObservableCollection<Card> ObservableHandCollection { get => _observableHandCollection;}

        public Hand()
        {
            _cards = new List<Card>();
            _observableHandCollection = new ObservableCollection<Card>();
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
            AddCardToObservableHand(card);
            _numberOfCards++;
        }

        private void AddCardToObservableHand(Card card)
        {
            _observableHandCollection.Add(card);
        }

        public List<Card> GetCurrentCards()
        {
            return _cards;
        }

        private void Clear()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Card card in _cards)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }

        public void PrintHandCollection()
        {
            foreach (Card card in _observableHandCollection)
            {
                Debug.WriteLine("Handcollection:" + card.ToString());
            }
        }
    }
}
