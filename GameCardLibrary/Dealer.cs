using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    public class Dealer
    {
        private string _name;
        private Hand _hand;

        
        public bool IsFinished { get; set; }
        public bool Winner { get; set; }
        public int Wins { get; set; }
        public int Blackjacks { get; set; }
        public int Busts { get; set; }
        public int Ties { get; set; }
        public int Losses { get; set; }
        [Key]
        public string Name { get => _name; set => _name = value; }
        public Hand Hand { get => _hand; set => _hand = value; }

        public Dealer(Hand hand)
        {
            Name = "Croupier";
            _hand = hand;
        }
    }
}
