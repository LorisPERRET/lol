using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Bio = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Familly = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    ChampionForeignKey = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skins_Champions_ChampionForeignKey",
                        column: x => x.ChampionForeignKey,
                        principalTable: "Champions",
                        principalColumn: "Name");
                });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "Name", "Bio", "Class", "Icon", "Image" },
                values: new object[,]
                {
                    { "Aatrox", "", "Fighter", "", "" },
                    { "Akali", "", "Assassin", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Runes",
                columns: new[] { "Id", "Description", "Familly", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "", "Precision", "", "Conqueror" },
                    { 2, "", "Precision", "", "Legend: Alacrity" },
                    { 3, "", "Domination", "", "last stand 2" }
                });

            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "ChampionForeignKey", "Description", "Icon", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Akali", "", "", "", "Stinger", 0f },
                    { 2, "Akali", "", "", "", "Infernal", 0f },
                    { 3, "Akali", "", "", "", "All-Star", 0f },
                    { 4, "Aatrox", "", "", "", "Justicar", 0f },
                    { 5, "Aatrox", "", "", "", "Mecha", 0f },
                    { 6, "Aatrox", "", "", "", "Sea Hunter", 0f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skins_ChampionForeignKey",
                table: "Skins",
                column: "ChampionForeignKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "Skins");

            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
