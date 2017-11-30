namespace StarCraft.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BuildingImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Buildings",
                maxLength: 5242880,
                nullable: false,
                defaultValue: new byte[] { });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Buildings");
        }
    }
}