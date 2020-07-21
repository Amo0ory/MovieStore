using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookStore.Migrations
{
    public partial class AddIsSubscribedToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedToNewsLetter",
                table: "CustomersDb",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribedToNewsLetter",
                table: "CustomersDb");
        }
    }
}
