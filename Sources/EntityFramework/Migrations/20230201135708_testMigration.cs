using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class testMigration : Migration
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
                    Class = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "Name", "Bio", "Class", "Icon" },
                values: new object[,]
                {
                    { "Aatrox", "", "Fighter", "" },
                    { "Ahri", "", "Mage", "" },
                    { "Akali", "", "Assassin", "" },
                    { "Akshan", "", "Marksman", "" },
                    { "Alistar", "", "Tank", "" },
                    { "Bard", "", "Support", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
