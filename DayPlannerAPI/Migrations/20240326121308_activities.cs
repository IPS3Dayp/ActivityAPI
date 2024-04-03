using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class activities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedActivities_Users_UserId",
                table: "PlannedActivities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PlannedActivities_UserId",
                table: "PlannedActivities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannedActivities_UserId",
                table: "PlannedActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedActivities_Users_UserId",
                table: "PlannedActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
