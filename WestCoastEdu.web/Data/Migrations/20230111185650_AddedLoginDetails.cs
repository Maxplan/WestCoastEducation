using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WestCoastEdu.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLoginDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Accounts");
        }
    }
}
