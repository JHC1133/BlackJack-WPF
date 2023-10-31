using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class Game
    {
        // Autogenerates an ID everytime a Game gets added to the DB
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime DatePlayed { get; set; }

        // Navigation properties

        //[ForeignKey("PlayerName")]
        //public string PlayerName { get; set; }

        // Navigation property
        public List<GamePlayerStatisticsIntermediary> GamePlayerStatisticsIntermediary { get; set; }

        [ForeignKey("Name")]
        public string DealerName { get; set; }
        public DealerStatistics DealerStatistics { get; set; }
    }
}
