using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    internal class Dealer
    {
        private string name;

        public Hand Hand { get; set; }
        public bool IsFinished { get; set; }
        public bool Winner { get; set; }


        public Dealer(Hand hand)
        {
            name = "Croupier";
        }
    }
}
