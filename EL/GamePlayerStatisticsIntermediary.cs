using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class GamePlayerStatisticsIntermediary
    {
        [Key]
        public int GamePlayerStatisticsID { get; set; }

        // Foreign keys
        [ForeignKey("Game")]
        public int GameID { get; set; }

        [ForeignKey("PlayerStatistics")]
        public string PlayerName { get; set; }

        // Navigation properties
        public Game Game { get; set; }
        public PlayerStatistics PlayerStatistics { get; set; }

    }
}
