using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookStore.Migrations
{
    public partial class AddDOBToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DOB",
                table: "CustomersDb",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "CustomersDb");
        }
    }
}
