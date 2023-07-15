using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hamro_Pasal.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Neighbourhood",
                table: "tbl_location");

            migrationBuilder.AddColumn<string>(
                name: "AdAddress",
                table: "tbl_ads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdAddress",
                table: "tbl_ads");

            migrationBuilder.AddColumn<string>(
                name: "Neighbourhood",
                table: "tbl_location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
