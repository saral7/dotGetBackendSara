using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokusaj.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Students",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "profilePictureURL",
                table: "Students",
                newName: "ProfilePictureURL");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Students",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Students",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Students",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Students",
                newName: "profilePictureURL");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Students",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "email");
        }
    }
}
