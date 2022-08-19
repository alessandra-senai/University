using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Requisito = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorHora = table.Column<double>(type: "float", nullable: true),
                    Certificados = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaPeriodo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Periodo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaPeriodo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInstrutor = table.Column<int>(type: "int", nullable: false),
                    CourseTeamId = table.Column<int>(type: "int", nullable: false),
                    CourseAwayId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CargaHoraria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Curso_CourseAwayId",
                        column: x => x.CourseAwayId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turma_Curso_CourseTeamId",
                        column: x => x.CourseTeamId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turma_Instrutor_IdInstrutor",
                        column: x => x.IdInstrutor,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTurma = table.Column<int>(type: "int", nullable: false),
                    IdAluno = table.Column<int>(type: "int", nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matricula_Aluno_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matricula_Turma_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdMatricula = table.Column<int>(type: "int", nullable: false),
                    IdNotaPeriodo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nota_Matricula_IdMatricula",
                        column: x => x.IdMatricula,
                        principalTable: "Matricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nota_NotaPeriodo_IdNotaPeriodo",
                        column: x => x.IdNotaPeriodo,
                        principalTable: "NotaPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "DataNascimento", "CPF", "Email", "Nome", "Telefone" },
                values: new object[] { 1, new DateTime(2010, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "111.222.333.40-0", "aluno1@gmail.com", "Aluno 1", "2132-3344" });

            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "DataNascimento", "CPF", "Email", "Nome", "Telefone" },
                values: new object[] { 2, new DateTime(2010, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "222.222.333.40-0", "aluno2@gmail.com", "Aluno 2", "4556-3344" });

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_IdAluno",
                table: "Matricula",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_IdTurma",
                table: "Matricula",
                column: "IdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_IdMatricula",
                table: "Nota",
                column: "IdMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_IdNotaPeriodo",
                table: "Nota",
                column: "IdNotaPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_CourseAwayId",
                table: "Turma",
                column: "CourseAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_CourseTeamId",
                table: "Turma",
                column: "CourseTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_IdInstrutor",
                table: "Turma",
                column: "IdInstrutor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nota");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "NotaPeriodo");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Instrutor");
        }
    }
}
