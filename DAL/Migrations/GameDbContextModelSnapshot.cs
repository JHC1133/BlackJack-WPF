﻿// <auto-generated />
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EL.DealerStatistics", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Blackjacks")
                        .HasColumnType("int");

                    b.Property<int>("Busts")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("Ties")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.ToTable("DealerStatistics");
                });

            modelBuilder.Entity("EL.PlayerStatistics", b =>
                {
                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Blackjacks")
                        .HasColumnType("int");

                    b.Property<int>("Busts")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("Ties")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("PlayerName");

                    b.ToTable("PlayerStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
