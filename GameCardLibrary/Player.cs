using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    internal class Player
    {

        public Hand Hand { get; set; }
        public bool IsFinished { get; set; }
        public bool Winner { get; set; }
        public string Name { get; set; }
        public string PlayerID { get; set; }


        public Player(string id, string name, Hand hand)
        {
            
        }


        public override string ToString()
        {
            return string.Empty;
        }

    }
}
