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
        public DateTime DatePlayed { get; set; }
        public DealerStatistics DealerStatistics { get; set; }
        public ICollection<PlayerStatistics> PlayerStatistics { get; set; }

    }
}
