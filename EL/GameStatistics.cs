using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class GameStatistics
    {

        [Key]
        public int ID { get; set; }

        // Foreign key properties
        [ForeignKey("Game")]
        public int GameID { get; set; }

        [ForeignKey("PlayerStatistics")]
        public string PlayerName { get; set; }

        [ForeignKey("DealerStatistics")]
        public string DealerName { get; set; }



        // Navigation properties for the many-to-many relationship
        public Game Game { get; set; }
        public PlayerStatistics PlayerStatistics { get; set; }
        public DealerStatistics DealerStatistics { get; set; }

    }
}
