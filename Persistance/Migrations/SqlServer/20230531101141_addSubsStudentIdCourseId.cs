using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class addSubsStudentIdCourseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Subs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Subs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subs_CourseId",
                table: "Subs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Subs_StudentId",
                table: "Subs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subs_Courses_CourseId",
                table: "Subs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Subs_Students_StudentId",
                table: "Subs",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subs_Courses_CourseId",
                table: "Subs");

            migrationBuilder.DropForeignKey(
                name: "FK_Subs_Students_StudentId",
                table: "Subs");

            migrationBuilder.DropIndex(
                name: "IX_Subs_CourseId",
                table: "Subs");

            migrationBuilder.DropIndex(
                name: "IX_Subs_StudentId",
                table: "Subs");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Subs");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subs");
        }
    }
}
