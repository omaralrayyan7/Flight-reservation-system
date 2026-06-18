using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationApp_f.Migrations.Data
{
    /// <inheritdoc />
    public partial class AddPriceToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Passenger",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Flight",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flight");
        }
    }
}
