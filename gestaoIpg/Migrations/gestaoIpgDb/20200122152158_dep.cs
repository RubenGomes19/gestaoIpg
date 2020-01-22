using Microsoft.EntityFrameworkCore.Migrations;

namespace gestaoIpg.Migrations.gestaoIpgDb
{
    public partial class dep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Funcionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DepartamentoId",
                table: "Funcionario",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_DepartamentoId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Funcionario");
        }
    }
}
