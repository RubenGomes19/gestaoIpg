using Microsoft.EntityFrameworkCore.Migrations;

namespace gestaoIpg.Migrations.gestaoIpgDb
{
    public partial class depch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeCargo",
                table: "Cargo",
                maxLength: 248,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeCargo",
                table: "Cargo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 248);
        }
    }
}
