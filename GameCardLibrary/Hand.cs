using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class Hand : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _numberOfCards;
        private List<Card> _cards;
        private int _score;

        ObservableCollection<Card> _observableHandCollection;
        public Card LastCard { get; }
        public int NumberOfCards { get => _numberOfCards; }
        //public int Score { get => _score; set => _score = value; } // Lambda?

        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

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

            OnPropertyChanged();
            return value;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
            _observableHandCollection.Add(card);
            _numberOfCards++;
            _score = CalculateHandValue();
        }

        public List<Card> GetCurrentCards()
        {
            return _cards;
        }

        public void Clear()
        {
            _cards.Clear();
            _observableHandCollection.Clear();
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
