using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookStore.Migrations
{
    public partial class AddMembershipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "MembershipTypeId",
                table: "CustomersDb",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "MembershipType",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    SignUpFee = table.Column<short>(nullable: false),
                    DurationInMonth = table.Column<byte>(nullable: false),
                    DiscountRate = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersDb_MembershipTypeId",
                table: "CustomersDb",
                column: "MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersDb_MembershipType_MembershipTypeId",
                table: "CustomersDb",
                column: "MembershipTypeId",
                principalTable: "MembershipType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersDb_MembershipType_MembershipTypeId",
                table: "CustomersDb");

            migrationBuilder.DropTable(
                name: "MembershipType");

            migrationBuilder.DropIndex(
                name: "IX_CustomersDb_MembershipTypeId",
                table: "CustomersDb");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                table: "CustomersDb");
        }
    }
}
