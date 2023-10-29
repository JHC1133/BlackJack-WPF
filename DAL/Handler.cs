using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Handler
    {

        public PlayerStatistics GetPlayerStatistics(string playerName)
        {
            using (var context = new GameDbContext())
            {
                PlayerStatistics playerStats = context.PlayerStatistics.FirstOrDefault(p => p.PlayerName == playerName);

                return playerStats;
            }
        }

        public void UpdatePlayerStatistics(PlayerStatistics playerStats)
        {
            using (var context = new GameDbContext())
            {
                context.PlayerStatistics.Update(playerStats);
                context.SaveChanges();
            }
        }

        public DealerStatistics GetDealerStatistics()
        {
            using (var context = new GameDbContext()) 
            {
              
                DealerStatistics dealerStats = context.DealerStatistics.FirstOrDefault();

                return dealerStats;
            }
        }

        public void UpdateDealerStatistics(DealerStatistics dealerStats)
        {
            using (var context = new GameDbContext())
            {

                context.DealerStatistics.Update(dealerStats);
                context.SaveChanges();
            }
        }

    }
}
