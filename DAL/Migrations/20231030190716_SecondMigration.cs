using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "PlayerStatistics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealerStatisticsName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_DealerStatistics_DealerStatisticsName",
                        column: x => x.DealerStatisticsName,
                        principalTable: "DealerStatistics",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_GameID",
                table: "PlayerStatistics",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_GameID",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "PlayerStatistics");
        }
    }
}
