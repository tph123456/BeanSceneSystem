using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanSceneSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_TableStatus",
                table: "Tables");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_TableStatus",
                table: "Tables",
                sql: "TableStatus IN ('Booked', 'Free')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_TableStatus",
                table: "Tables");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_TableStatus",
                table: "Tables",
                sql: "TableStatus IN ('Seated', 'Free')");
        }
    }
}
