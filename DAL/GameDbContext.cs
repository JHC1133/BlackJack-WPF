using EL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class GameDbContext : DbContext
    {

        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public DbSet<DealerStatistics> DealerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayerStatisticsIntermediary> GamePlayerStatisticsIntermediary { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>().Property(g => g.ID).ValueGeneratedOnAdd();

            // Configure the Game entity
            modelBuilder.Entity<Game>()
                .HasKey(g => g.ID);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.DealerStatistics)
                .WithMany()
                .HasForeignKey(g => g.DealerName);

            // Configure the PlayerStatistics entity
            modelBuilder.Entity<PlayerStatistics>()
                .HasKey(ps => ps.PlayerName);

            // Configure the many-to-many relationship using the intermediary entity
            modelBuilder.Entity<GamePlayerStatisticsIntermediary>()
                .HasKey(gps => new { gps.GameID, gps.PlayerName });

            modelBuilder.Entity<GamePlayerStatisticsIntermediary>()
                .HasOne(gps => gps.Game)
                .WithMany(g => g.GamePlayerStatisticsIntermediary)
                .HasForeignKey(gps => gps.GameID);

            modelBuilder.Entity<GamePlayerStatisticsIntermediary>()
                .HasOne(gps => gps.PlayerStatistics)
                .WithMany(ps => ps.GamesPlayerIntermediary)
                .HasForeignKey(gps => gps.PlayerName);

            base.OnModelCreating(modelBuilder);
        }

    }
}
