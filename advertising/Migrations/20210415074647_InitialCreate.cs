using Microsoft.EntityFrameworkCore.Migrations;

namespace advertising.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_advertisers",
                columns: table => new
                {
                    adv_advertiser_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    adv_phone_number = table.Column<string>(type: "TEXT", nullable: false),
                    adv_distribution_address = table.Column<string>(type: "TEXT", nullable: false),
                    adv_distribution_zip_code = table.Column<string>(type: "TEXT", nullable: false),
                    adv_distribution_city = table.Column<string>(type: "TEXT", nullable: false),
                    adv_invoice_address = table.Column<string>(type: "TEXT", nullable: false),
                    adv_invoice_zip_code = table.Column<string>(type: "TEXT", nullable: false),
                    adv_invoice_city = table.Column<string>(type: "TEXT", nullable: false),
                    adv_is_organization = table.Column<bool>(type: "INTEGER", nullable: false),
                    adv_first_name = table.Column<string>(type: "TEXT", nullable: true),
                    adv_last_name = table.Column<string>(type: "TEXT", nullable: true),
                    adv_subscriber_id = table.Column<long>(type: "INTEGER", nullable: true),
                    adv_organization_name = table.Column<string>(type: "TEXT", nullable: true),
                    adv_organization_number = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_advertisers", x => x.adv_advertiser_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ads",
                columns: table => new
                {
                    ad_ad_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ad_price = table.Column<int>(type: "INTEGER", nullable: false),
                    ad_content = table.Column<string>(type: "TEXT", nullable: false),
                    ad_headline = table.Column<string>(type: "TEXT", nullable: false),
                    ad_ad_cost = table.Column<int>(type: "INTEGER", nullable: false),
                    ad_fk_advertiser_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ads", x => x.ad_ad_id);
                    table.ForeignKey(
                        name: "FK_tbl_ads_tbl_advertisers_ad_fk_advertiser_id",
                        column: x => x.ad_fk_advertiser_id,
                        principalTable: "tbl_advertisers",
                        principalColumn: "adv_advertiser_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ads_ad_fk_advertiser_id",
                table: "tbl_ads",
                column: "ad_fk_advertiser_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ads");

            migrationBuilder.DropTable(
                name: "tbl_advertisers");
        }
    }
}
