using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    internal class Player
    {

        Hand Hand { get; set; }
        bool IsFinished { get; set; }
        bool Winner { get; set; }
        string Name { get; set; }
        string PlayerID { get; set; }


        public Player(string id, string name, Hand hand)
        {
            
        }


        public override string ToString()
        {
            return string.Empty;
        }

    }
}
