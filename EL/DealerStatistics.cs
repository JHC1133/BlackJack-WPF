using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class DealerStatistics
    {
        public int Wins { get; set; }
        public int Blackjacks { get; set; }
        public int Busts { get; set; }
        public int Ties { get; set; }
        public int Losses { get; set; }

        [Key]
        public string Name { get; set; }


        // Navigation property for the join table
        public List<GameStatistics> GameStatistics { get; set; }
    }
}
