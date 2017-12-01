namespace StarCraft.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UnitsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Buildings",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Damage = table.Column<int>(nullable: false),
                    GasCost = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(maxLength: 5242880, nullable: false),
                    MineralCost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Race = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingUnit",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingUnit", x => new { x.BuildingId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_BuildingUnit_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingUnit_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingUnit_UnitId",
                table: "BuildingUnit",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingUnit");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Buildings",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}