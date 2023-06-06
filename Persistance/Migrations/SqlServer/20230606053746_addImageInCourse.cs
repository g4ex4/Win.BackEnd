using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class addImageInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "ImageCourseUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCourseUrl",
                table: "Courses");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "Courses",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
