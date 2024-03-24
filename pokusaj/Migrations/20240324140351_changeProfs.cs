using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokusaj.Migrations
{
    /// <inheritdoc />
    public partial class changeProfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Professors",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Professors");
        }
    }
}
