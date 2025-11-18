using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Interação_Medicamentosa.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Fabricante = table.Column<string>(type: "text", nullable: true),
                    PrecoMin = table.Column<decimal>(type: "numeric", nullable: false),
                    PrecoMax = table.Column<decimal>(type: "numeric", nullable: false),
                    FormaFarmaceutica = table.Column<string>(type: "text", nullable: true),
                    ClasseTerapeutica = table.Column<string>(type: "text", nullable: true),
                    Indicacoes = table.Column<string>(type: "text", nullable: true),
                    Armazenamento = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    UltimaVisita = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CondicoesMedicas = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interações",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descrição = table.Column<string>(type: "text", nullable: true),
                    Substancia = table.Column<string>(type: "text", nullable: true),
                    Risco = table.Column<string>(type: "text", nullable: true),
                    MedicamentoId_1 = table.Column<int>(type: "integer", nullable: false),
                    MedicamentoId_2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interações", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interações_Medicamentos_MedicamentoId_1",
                        column: x => x.MedicamentoId_1,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interações_Medicamentos_MedicamentoId_2",
                        column: x => x.MedicamentoId_2,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescrições",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    IdMedicamento = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    HorariodoRemedio = table.Column<int>(type: "integer", nullable: false),
                    Dosagem = table.Column<int>(type: "integer", nullable: false),
                    frequencia = table.Column<int>(type: "integer", nullable: false),
                    Duracao = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescrições", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescrições_Medicamentos_IdMedicamento",
                        column: x => x.IdMedicamento,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescrições_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interações_MedicamentoId_1",
                table: "Interações",
                column: "MedicamentoId_1");

            migrationBuilder.CreateIndex(
                name: "IX_Interações_MedicamentoId_2",
                table: "Interações",
                column: "MedicamentoId_2");

            migrationBuilder.CreateIndex(
                name: "IX_Prescrições_IdMedicamento",
                table: "Prescrições",
                column: "IdMedicamento");

            migrationBuilder.CreateIndex(
                name: "IX_Prescrições_IdPaciente",
                table: "Prescrições",
                column: "IdPaciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interações");

            migrationBuilder.DropTable(
                name: "Prescrições");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
