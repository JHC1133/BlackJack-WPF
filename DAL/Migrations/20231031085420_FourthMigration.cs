using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DealerStatistics_DealerStatisticsName",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_GameID",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "DealerStatisticsName",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => new { x.GameID, x.PlayerName, x.DealerName });
                    table.ForeignKey(
                        name: "FK_GameStatistics_DealerStatistics_DealerName",
                        column: x => x.DealerName,
                        principalTable: "DealerStatistics",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistics_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistics_PlayerStatistics_PlayerName",
                        column: x => x.PlayerName,
                        principalTable: "PlayerStatistics",
                        principalColumn: "PlayerName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_DealerName",
                table: "GameStatistics",
                column: "DealerName");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_PlayerName",
                table: "GameStatistics",
                column: "PlayerName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "PlayerStatistics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerStatisticsName",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_GameID",
                table: "PlayerStatistics",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DealerStatistics_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName",
                principalTable: "DealerStatistics",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID");
        }
    }
}
