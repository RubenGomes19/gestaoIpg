using Microsoft.EntityFrameworkCore.Migrations;

namespace gestaoIpg.Migrations.gestaoIpgDb
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "Funcionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_CargoId",
                table: "Funcionario",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Cargo_CargoId",
                table: "Funcionario",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Cargo_CargoId",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_CargoId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Funcionario");
        }
    }
}
