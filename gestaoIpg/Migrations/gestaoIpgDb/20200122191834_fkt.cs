using Microsoft.EntityFrameworkCore.Migrations;

namespace gestaoIpg.Migrations.gestaoIpgDb
{
    public partial class fkt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FuncionarioId",
                table: "Tarefa",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Funcionario_FuncionarioId",
                table: "Tarefa",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Funcionario_FuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Tarefa");
        }
    }
}
