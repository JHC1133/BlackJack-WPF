using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class PlayerStatistics
    {
        public int Wins { get; set; }
        public int Blackjacks { get; set; }
        public int Busts { get; set; }
        public int Ties { get; set; }
        public int Losses { get; set; }

        [Key]
        public string PlayerName { get; set; }

    }
}
