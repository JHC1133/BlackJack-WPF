using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class Game
    {

        public int ID { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}
