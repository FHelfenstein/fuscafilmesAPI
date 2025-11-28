using FuscaFilmes.API.Models;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointsHandlers {
    public static class FilmesHandlers {

        public static async Task<List<Filme>> GetFilmes(Context context) {

            return await context.Filmes
                .Include(x => x.Diretores)
                .OrderByDescending(x => x.Ano)
                .ThenBy(x => x.Titulo).ToListAsync();
        }

        public static async Task<List<Filme>> GetFilmesById(int id, Context context) {

            return await context.Filmes.Where(x => x.Id == id)
                .Include(x => x.Diretores)
                .ToListAsync();
        }

        public static async Task<List<Filme>> GetFilmesEFFunctionsByName(string titulo, Context context) {

            return await context.Filmes.Where(x =>
                EF.Functions.Like(x.Titulo, $"%{titulo}%")
             )
             .Include(x => x.Diretores)
            .ToListAsync();
        }

        public static async Task<List<Filme>> GetFilmesContainsByName(string titulo, Context context) {

            return await context.Filmes.Where(x => x.Titulo.ToLower().Contains(titulo))
                .Include(x => x.Diretores)
                .ToListAsync();
        }

        public static async Task<IResult> UpdateFilme(Context context, FilmeUpdate filmeUpdate) {

            var filme = await context.Filmes.FindAsync(filmeUpdate.Id);

            if (filme == null) {
                return Results.NotFound("Filme não encontrado.");
            }

            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            context.Filmes.Update(filme);
            await context.SaveChangesAsync();

            return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso.");

        }

        public static async Task<IResult> UpdateExecuteFilme(Context context, FilmeUpdate filmeUpdate) {

            var linhasAfetadas = await context.Filmes
                .Where(x => x.Id == filmeUpdate.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
                    .SetProperty(f => f.Ano, filmeUpdate.Ano)
                );

            if (linhasAfetadas > 0) {
                return Results.Ok($"Você tem um total de {linhasAfetadas} linhas(s) afetada(s).");

            } else {
                return Results.NoContent();
            }
        }

        public static async Task DeleteFilme(int filmeId, Context context) {

            await context.Filmes.Where(x => x.Id == filmeId).ExecuteDeleteAsync<Filme>();
        }
    }
}
