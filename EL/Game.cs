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


        // Navigation property for the join table
        public List<GameStatistics> GameStatistics { get; set; }



        //public DealerStatistics DealerStatistics { get; set; }
        //public List<PlayerStatistics> PlayerStatistics { get; set; }

        //[ForeignKey("PlayerStatistics")]
        //public string PlayerName { get; set; }

        //[ForeignKey("DealerStatistics")]
        //public string Name { get; set; }

    }
}
