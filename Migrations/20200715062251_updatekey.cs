using Microsoft.EntityFrameworkCore.Migrations;

namespace LogApi.Migrations
{
    public partial class updatekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string));
        }
    }
}
