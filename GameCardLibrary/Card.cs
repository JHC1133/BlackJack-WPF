using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    internal class Card
    {

        public Suit Suit { get; set; }
        public Value Value { get; set; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        public int GetCardValue()
        {
            if (Value == Value.Jack || Value == Value.Queen || Value == Value.King)
            {
                return 10;
            }
            return (int)Value;
        }

        public override string ToString()
        {
            return $"{Value} {this.GetCardValue()} of {Suit}";
        }
    }
}
