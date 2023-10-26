using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class Card
    {
        private string BoundText = "bounded from Hand";
        public string BoundText1 { get => BoundText; set => BoundText = value; }
        public Suit Suit { get; set; }
        public Value Value { get; set; }
        public string ImagePath { get; set; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;

            ImagePath = $"Assets/Cards/{(int)Value}{Suit.ToString()[0]}";
        }

        public int GetCardValue()
        {
            if (Value == Value.Jack || Value == Value.Queen || Value == Value.King)
            {
                return 10;
            }
            else if (Value == Value.Ace)
            {
                return 11;
            }
            return (int)Value;
        }


        public override string ToString()
        {
            return $"{Value} {this.GetCardValue()} of {Suit} with ImagePath {ImagePath}";
        }
    }
}
