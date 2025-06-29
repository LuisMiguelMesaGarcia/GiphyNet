using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GifAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CatFact = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    GifUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchQueryWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchHistoryId = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WordOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueryWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchQueryWords_SearchHistories_SearchHistoryId",
                        column: x => x.SearchHistoryId,
                        principalTable: "SearchHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistories_SearchDate",
                table: "SearchHistories",
                column: "SearchDate");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueryWords_SearchHistoryId_WordOrder",
                table: "SearchQueryWords",
                columns: new[] { "SearchHistoryId", "WordOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueryWords_Word",
                table: "SearchQueryWords",
                column: "Word");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchQueryWords");

            migrationBuilder.DropTable(
                name: "SearchHistories");
        }
    }
}
