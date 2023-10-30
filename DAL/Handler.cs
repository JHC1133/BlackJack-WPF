using EL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Handler
    {

        public void AddGame(Game game)
        {
            using (var context = new GameDbContext())
            {
                context.Games.Add(game);
                context.SaveChanges();
            }
        }

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
                // Retrieve the existing PlayerStatistics record from the database
                var existingPlayerStats = context.PlayerStatistics.FirstOrDefault(p => p.PlayerName == playerStats.PlayerName);

                if (existingPlayerStats != null)
                {
                    existingPlayerStats.Wins += playerStats.Wins;
                    existingPlayerStats.Busts += playerStats.Busts;
                    existingPlayerStats.Ties += playerStats.Ties;
                    existingPlayerStats.Losses += playerStats.Losses;
                    existingPlayerStats.Blackjacks += playerStats.Blackjacks;

                    context.SaveChanges();
                }
                else
                {
                    context.PlayerStatistics.Add(playerStats);
                }

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
                // Retrieve the existing DealerStatistics record from the database
                var existingDealerStats = context.DealerStatistics.FirstOrDefault();

                if (existingDealerStats != null)
                {
                    existingDealerStats.Wins += dealerStats.Wins;
                    existingDealerStats.Busts += dealerStats.Busts;
                    existingDealerStats.Ties += dealerStats.Ties;
                    existingDealerStats.Losses += dealerStats.Losses;
                    existingDealerStats.Blackjacks += dealerStats.Blackjacks;

                    context.SaveChanges();
                }
                else
                {
                    context.DealerStatistics.Add(dealerStats);
                    context.SaveChanges();
                }

                PrintDealerTableContent();
            }
        }

        public void RemoveItemFromTable(string playerName)
        {
            using (var context = new GameDbContext())
            {

                var playerToRemove = (from player in context.PlayerStatistics
                                      where player.PlayerName == playerName
                                      select player).SingleOrDefault();

                if (playerToRemove != null)
                {
                    context.PlayerStatistics.Remove(playerToRemove);
                    context.SaveChanges();
                    Debug.WriteLine($"{playerToRemove} has been removed from the table");

                    PrintPlayerTableContent();
                }
            }

            if (playerName == "Croupier")
            {
                using (var context = new GameDbContext())
                {

                    var playerToRemove = (from player in context.DealerStatistics
                                          where player.Name == playerName
                                          select player).SingleOrDefault();

                    if (playerToRemove != null)
                    {
                        context.DealerStatistics.Remove(playerToRemove);
                        context.SaveChanges();
                        Debug.WriteLine($"{playerToRemove} has been removed from the table");

                        PrintPlayerTableContent();
                    }
                }
            }
        }

        private void PrintDealerTableContent()
        {
            using (var context = new GameDbContext())
            {
                var dealerStats = context.DealerStatistics.SingleOrDefault();

                Debug.WriteLine($"Dealer Name: {dealerStats.Name}");
                Debug.WriteLine($"Wins: {dealerStats.Wins}");
                Debug.WriteLine($"Busts: {dealerStats.Busts}");
                Debug.WriteLine($"Ties: {dealerStats.Ties}");
                Debug.WriteLine($"Losses: {dealerStats.Losses}");
                Debug.WriteLine($"Blackjacks: {dealerStats.Blackjacks}");
                Debug.WriteLine("");
            }
        }

        
        public void PrintPlayerTableContent()
        {
            using (var context = new GameDbContext())
            {
                var playerStats = context.PlayerStatistics.ToList();

                foreach (var playerStat in playerStats)
                {
                    Debug.WriteLine($"Player Name: {playerStat.PlayerName}");
                    Debug.WriteLine($"Wins: {playerStat.Wins}");
                    Debug.WriteLine($"Busts: {playerStat.Busts}");
                    Debug.WriteLine($"Ties: {playerStat.Ties}");
                    Debug.WriteLine($"Losses: {playerStat.Losses}");
                    Debug.WriteLine($"Blackjacks: {playerStat.Blackjacks}");
                    Debug.WriteLine("Playertable printed");
                }
            }

                    Debug.WriteLine("");
        }


    }
}
