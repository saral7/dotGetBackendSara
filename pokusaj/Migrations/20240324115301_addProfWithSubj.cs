using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokusaj.Migrations
{
    /// <inheritdoc />
    public partial class addProfWithSubj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Professors");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Professors",
                type: "TEXT",
                nullable: true);
        }
    }
}
