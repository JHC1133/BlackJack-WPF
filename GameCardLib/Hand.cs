using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    internal class Hand
    {

        Deck deck;

        public Card LastCard { get; }
        public int NumberOfCards { get; }
        public int Score { get; }

        public Hand(Deck deck)
        {
            
        }

        private void AddCard(Card card)
        {

        }

        private void Clear()
        {

        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
