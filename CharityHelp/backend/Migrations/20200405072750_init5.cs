using Microsoft.EntityFrameworkCore.Migrations;

namespace charity.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    AssignedUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_AssignedUserId",
                table: "Volunteers",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_DeviceId",
                table: "Volunteers",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteers");
        }
    }
}
