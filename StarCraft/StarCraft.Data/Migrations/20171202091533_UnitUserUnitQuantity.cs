namespace StarCraft.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UnitUserUnitQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "UnitUser",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "UnitUser");
        }
    }
}