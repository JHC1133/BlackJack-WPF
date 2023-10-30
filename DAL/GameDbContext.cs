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
        //public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameDB");
        }

    }
}
