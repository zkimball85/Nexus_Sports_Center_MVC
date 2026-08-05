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
            // Add VenueId to Teams (keep existing logic)
            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Teams",
                type: "int",
                nullable: true);

            // Add new FirstName and LastName columns
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            // Migrate existing data by splitting FullName at the first space
            migrationBuilder.Sql(@"
                UPDATE Players
                SET 
                    FirstName = CASE 
                        WHEN CHARINDEX(' ', FullName) > 0 THEN SUBSTRING(FullName, 1, CHARINDEX(' ', FullName) - 1)
                        ELSE FullName 
                    END,
                    LastName = CASE 
                        WHEN CHARINDEX(' ', FullName) > 0 THEN SUBSTRING(FullName, CHARINDEX(' ', FullName) + 1, LEN(FullName))
                        ELSE '' 
                    END
            ");

            // 4. Drop the legacy FullName column
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Players");
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
