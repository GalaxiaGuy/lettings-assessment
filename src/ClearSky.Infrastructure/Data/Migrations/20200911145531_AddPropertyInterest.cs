using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSky.Infrastructure.Data.Migrations
{
    public partial class AddPropertyInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyInterest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyId = table.Column<string>(nullable: true),
                    CustomerIdentityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInterest_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInterest_PropertyId",
                table: "PropertyInterest",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyInterest");
        }
    }
}
