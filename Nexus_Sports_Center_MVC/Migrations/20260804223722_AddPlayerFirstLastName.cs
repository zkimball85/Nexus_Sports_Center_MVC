using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus_Sports_Center_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerFirstLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Players",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_VenueId",
                table: "Teams",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Venues_VenueId",
                table: "Teams",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Venues_VenueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_VenueId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "FullName");
        }
    }
}
