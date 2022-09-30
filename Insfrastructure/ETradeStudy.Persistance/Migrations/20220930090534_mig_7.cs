using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETradeStudy.Percistance.Migrations
{
    public partial class mig_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenDate",
                table: "AspNetUsers",
                newName: "RefreshTokenEndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenEndDate",
                table: "AspNetUsers",
                newName: "RefreshTokenDate");
        }
    }
}
