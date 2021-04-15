using Microsoft.EntityFrameworkCore.Migrations;

namespace advertising.Migrations
{
    public partial class AddedPersonalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "adv_personal_id",
                table: "tbl_advertisers",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adv_personal_id",
                table: "tbl_advertisers");
        }
    }
}
