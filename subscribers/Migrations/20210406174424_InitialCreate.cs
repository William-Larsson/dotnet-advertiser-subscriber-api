using Microsoft.EntityFrameworkCore.Migrations;

namespace subscribers.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_subscribers",
                columns: table => new
                {
                    sub_subscriber_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    sub_personal_id = table.Column<int>(type: "INTEGER", nullable: false),
                    sub_first_name = table.Column<string>(type: "TEXT", nullable: false),
                    sub_last_name = table.Column<string>(type: "TEXT", nullable: false),
                    sub_distribution_address = table.Column<string>(type: "TEXT", nullable: false),
                    sub_zip_code = table.Column<string>(type: "TEXT", nullable: false),
                    sub_city = table.Column<string>(type: "TEXT", nullable: false),
                    sub_phone_number = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subscribers", x => x.sub_subscriber_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_subscribers");
        }
    }
}
