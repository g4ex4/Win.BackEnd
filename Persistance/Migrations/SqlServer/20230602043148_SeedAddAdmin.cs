using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class SeedAddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
           table: "Employees",
           columns: new[] { "Id", "UserName", "Email", "PasswordHash", "EmailConfirmed",
                "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount",
                "IsConfirmed", "JobTitle", "Experience", "Education", "DateTimeAdded", "DateTimeUpdated",
                "RoleId" },
           values: new object[,]
           {
                    { 1, "Admin", "1goldyshsergei1@gmail.com",
                "AQAAAAEAACcQAAAAEN5fWHtswlk5nb29qq19SUqB1JKlHcJt/dtzKi+7g8xYmrxBOJ5VyRUtTUaaOu9aGw==",
                true, true, true, true, 0, true, "Administrator", "10 Year", "IT Academy",
                DateTime.Now, DateTime.Now, 1 }

           });
            
        }

    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
