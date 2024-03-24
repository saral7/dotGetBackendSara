using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokusaj.Migrations
{
    /// <inheritdoc />
    public partial class addSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subjects",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Subjects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Subjects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfsWithSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Subject = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfsWithSub", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfsWithSub");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Subjects",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Subjects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
