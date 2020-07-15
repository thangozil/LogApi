using Microsoft.EntityFrameworkCore.Migrations;

namespace LogApi.Migrations
{
    public partial class AutoGenKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValueSql: "NEWID()");
        }
    }
}
