using FuscaFilmes.API.Models;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointsHandlers {
    public static class FilmesHandlers {

        public static List<Filme> GetFilmes(Context context) {

            return context.Filmes
                .Include(x => x.Diretores)
                .OrderByDescending(x => x.Ano)
                .ThenBy(x => x.Titulo).ToList();
        }

        public static List<Filme> GetFilmesById(int id, Context context) {

            return context.Filmes.Where(x => x.Id == id)
                .Include(x => x.Diretores)
                .ToList();
        }

        public static List<Filme> GetFilmesEFFunctionsByName(string titulo, Context context) {

            return context.Filmes.Where(x =>
                EF.Functions.Like(x.Titulo, $"%{titulo}%")
             )
             .Include(x => x.Diretores)
            .ToList();
        }

        public static List<Filme> GetFilmesContainsByName(string titulo, Context context) {

            return context.Filmes.Where(x => x.Titulo.ToLower().Contains(titulo))
                .Include(x => x.Diretores)
                .ToList();
        }

        public static IResult UpdateFilme(Context context, FilmeUpdate filmeUpdate) {

            var filme = context.Filmes.Find(filmeUpdate.Id);

            if (filme == null) {
                return Results.NotFound("Filme não encontrado.");
            }

            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            context.Filmes.Update(filme);
            context.SaveChanges();

            return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso.");

        }

        public static IResult UpdateExecuteFilme(Context context, FilmeUpdate filmeUpdate) {

            var linhasAfetadas = context.Filmes
                .Where(x => x.Id == filmeUpdate.Id)
                .ExecuteUpdate(setter => setter
                    .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
                    .SetProperty(f => f.Ano, filmeUpdate.Ano)
                );

            if (linhasAfetadas > 0) {
                return Results.Ok($"Você tem um total de {linhasAfetadas} linhas(s) afetada(s).");

            } else {
                return Results.NoContent();
            }
        }

        public static void DeleteFilme(int filmeId, Context context) {

            context.Filmes.Where(x => x.Id == filmeId).ExecuteDelete<Filme>();
        }
    }
}
