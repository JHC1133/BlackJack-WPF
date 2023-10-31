using EL;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Creates a Game in the Game table
        /// </summary>
        /// <param name="playerNames"></param>
        /// <param name="dealerName"></param>
        public void CreateNewGame(List<string> playerNames, string dealerName)
        {
            using (var context = new GameDbContext())
            {
                var game = new Game
                {
                    DatePlayed = DateTime.Now,
                    DealerName = dealerName
                };

                game.DealerStatistics = new DealerStatistics
                {
                    Name = dealerName
                };

                foreach (var playerName in playerNames)
                {
                    game.PlayerStatistics.Add(new PlayerStatistics
                    {
                        PlayerName = playerName
                    });
                }

                context.Games.Add(game);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Updates the current game in the Game table. Should be called just before the shutting down of the Blackjack application
        /// </summary>
        public void UpdateGame()
        {
            using (var context = new GameDbContext())
            {
                // Gets the latest (current) game added
                var game = context.Games.Include(g => g.PlayerStatistics).Include(g => g.DealerStatistics).OrderByDescending(g => g.DatePlayed).FirstOrDefault();

                if (game == null)
                {
                    Debug.WriteLine("There was no game in the table");
                    return;
                }


                foreach (var playerStats in game.PlayerStatistics)
                {
                    var playerName = playerStats.PlayerName;
                    var updatedPlayerStats = GetPlayerStatistics(playerName);

                    if (updatedPlayerStats != null)
                    {
                        playerStats.Wins = updatedPlayerStats.Wins;
                        playerStats.Blackjacks = updatedPlayerStats.Blackjacks;
                        playerStats.Busts = updatedPlayerStats.Busts;
                        playerStats.Losses = updatedPlayerStats.Losses;
                        playerStats.Ties = updatedPlayerStats.Ties;
                    }
                }

                var dealerStats = game.DealerStatistics;

                if (dealerStats != null)
                {
                    var updatedDealerStats = GetDealerStatistics();

                    if (updatedDealerStats != null)
                    {
                        dealerStats.Wins = updatedDealerStats.Wins;
                        dealerStats.Blackjacks = updatedDealerStats.Blackjacks;
                        dealerStats.Busts = updatedDealerStats.Busts;
                        dealerStats.Losses = updatedDealerStats.Losses;
                        dealerStats.Ties = updatedDealerStats.Ties;
                    }
                }

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
                PrintGameTableContent();
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

        private void PrintGameTableContent()
        {
           
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
