using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokusaj.Migrations
{
    /// <inheritdoc />
    public partial class renamesProfilePictureProf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "Professors",
                newName: "ProfilePicture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Professors",
                newName: "ProfilePictureUrl");
        }
    }
}
