using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FuscaFilmes.Repo.Migrations
{
    /// <inheritdoc />
    public partial class _020_DiretorDetalhe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiretorDetalhe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Biografia = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DiretorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretorDetalhe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiretorDetalhe_Diretores_DiretorId",
                        column: x => x.DiretorId,
                        principalTable: "Diretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiretorDetalhe",
                columns: new[] { "Id", "Biografia", "DataNascimento", "DiretorId" },
                values: new object[,]
                {
                    { 1, "Biografia do diretor Christopher Nolan", new DateTime(1970, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Biografia do diretor Steven Spielberg", new DateTime(1952, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Biografia do Quentin Tarantino", new DateTime(1950, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Biografia do diretor Martin Scorsese", new DateTime(1968, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiretorDetalhe_DiretorId",
                table: "DiretorDetalhe",
                column: "DiretorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiretorDetalhe");
        }
    }
}
