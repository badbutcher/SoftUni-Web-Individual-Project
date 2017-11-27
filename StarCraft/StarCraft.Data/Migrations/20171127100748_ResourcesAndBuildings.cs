namespace StarCraft.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ResourcesAndBuildings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gas",
                table: "AspNetUsers",
                maxLength: 1000000,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minerals",
                table: "AspNetUsers",
                maxLength: 1000000,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GasCost = table.Column<int>(maxLength: 2000, nullable: false),
                    MineralCost = table.Column<int>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Race = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBuilding",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BuildingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBuilding", x => new { x.UserId, x.BuildingId });
                    table.ForeignKey(
                        name: "FK_UserBuilding_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBuilding_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBuilding_BuildingId",
                table: "UserBuilding",
                column: "BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBuilding");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropColumn(
                name: "Gas",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Minerals",
                table: "AspNetUsers");
        }
    }
}
