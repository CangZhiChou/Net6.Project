using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advanced.NET6.EFCore.DB.Migrations
{
    public partial class Init004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Company",
                newName: "CompanyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Company",
                newName: "Name");
        }
    }
}
