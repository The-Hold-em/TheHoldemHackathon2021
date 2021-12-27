using Microsoft.EntityFrameworkCore.Migrations;

namespace THH.IdentityServer.Migrations
{
    public partial class identityNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityNumber",
                table: "AspNetUsers",
                column: "IdentityNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityNumber",
                table: "AspNetUsers");
        }
    }
}
