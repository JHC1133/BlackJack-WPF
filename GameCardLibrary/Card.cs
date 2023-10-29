using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class Card
    {      
        public Suit Suit { get; set; }
        public Value Value { get; set; }
        public string ImagePath { get; set; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;

            //Sets Image according to naming format "[Value][FirstLetterOfSuit]", e.g. "10C" is 10 of clubs
            ImagePath = $"/Assets/Cards/{(int)Value}{Suit.ToString()[0]}.png";
        }


        /// <summary>
        /// Returns the Value of the card
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns string with name value, numerical value, Suit and ImagePath
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Value} {this.GetCardValue()} of {Suit} with ImagePath {ImagePath}";
        }
    }
}
