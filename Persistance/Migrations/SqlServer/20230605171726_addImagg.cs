using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class addImagg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Courses",
                newName: "ImageCourseName");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByte",
                table: "Courses",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ImageCourseName",
                table: "Courses",
                newName: "ImagePath");
        }
    }
}
