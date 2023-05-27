using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class EditmodelStudentsub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubscriptions",
                table: "StudentSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubscriptions_StudentId",
                table: "StudentSubscriptions");

            migrationBuilder.DropColumn(
                name: "StudenId",
                table: "StudentSubscriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubscriptions",
                table: "StudentSubscriptions",
                columns: new[] { "StudentId", "SubscriptionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubscriptions",
                table: "StudentSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "StudenId",
                table: "StudentSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubscriptions",
                table: "StudentSubscriptions",
                columns: new[] { "StudenId", "SubscriptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubscriptions_StudentId",
                table: "StudentSubscriptions",
                column: "StudentId");
        }
    }
}
