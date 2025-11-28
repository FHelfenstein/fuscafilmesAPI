using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FuscaFilmes.Repo.Migrations
{
    /// <inheritdoc />
    public partial class M2MCCreatingClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiretorFilme");

            migrationBuilder.CreateTable(
                name: "DiretoresFilmes",
                columns: table => new
                {
                    DiretorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretoresFilmes", x => new { x.DiretorId, x.FilmeId });
                    table.ForeignKey(
                        name: "FK_DiretoresFilmes_Diretores_DiretorId",
                        column: x => x.DiretorId,
                        principalTable: "Diretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiretoresFilmes_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diretores",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Greta Gerwig" });

            migrationBuilder.InsertData(
                table: "DiretoresFilmes",
                columns: new[] { "DiretorId", "FilmeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 4, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2008, "The Dark Knight" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1993, "Jurassic Park" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1982, "E.T. the Extra-Terrestrial" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1993, "Schindler's List" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1994, "Pulp Fiction" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2003, "Kill Bill: Vol. 1" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2012, "Django Unchained" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1990, "Goodfellas" });

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Ano", "Titulo" },
                values: new object[,]
                {
                    { 11, 2019, "The Irishman" },
                    { 12, 1976, "Taxi Driver" },
                    { 13, 2009, "Avatar" },
                    { 14, 1997, "Titanic" },
                    { 15, 1984, "The Terminator" },
                    { 16, 2017, "Lady Bird" },
                    { 17, 2019, "Little Women" },
                    { 18, 2023, "Barbie" }
                });

            migrationBuilder.InsertData(
                table: "DiretoresFilmes",
                columns: new[] { "DiretorId", "FilmeId" },
                values: new object[,]
                {
                    { 4, 11 },
                    { 4, 12 },
                    { 5, 13 },
                    { 5, 14 },
                    { 5, 15 },
                    { 6, 16 },
                    { 6, 17 },
                    { 6, 18 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresFilmes_FilmeId",
                table: "DiretoresFilmes",
                column: "FilmeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiretoresFilmes");

            migrationBuilder.DeleteData(
                table: "Diretores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.CreateTable(
                name: "DiretorFilme",
                columns: table => new
                {
                    DiretoresId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretorFilme", x => new { x.DiretoresId, x.FilmesId });
                    table.ForeignKey(
                        name: "FK_DiretorFilme_Diretores_DiretoresId",
                        column: x => x.DiretoresId,
                        principalTable: "Diretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiretorFilme_Filmes_FilmesId",
                        column: x => x.FilmesId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1993, "Jurassic Park" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1982, "E.T. the Extra-Terrestrial" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1994, "Pulp Fiction" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2003, "Kill Bill: Volume 1" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2013, "The Wolf of Wall Street" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2010, "Shutter Island" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 1997, "Titanic" });

            migrationBuilder.UpdateData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Ano", "Titulo" },
                values: new object[] { 2009, "Avatar" });

            migrationBuilder.CreateIndex(
                name: "IX_DiretorFilme_FilmesId",
                table: "DiretorFilme",
                column: "FilmesId");
        }
    }
}
