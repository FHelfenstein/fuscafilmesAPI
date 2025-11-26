using FuscaFilmes.API.EndpointsHandlers;

namespace FuscaFilmes.API.Extensions {
    public static class EndpointsFilmes {
        public static void FilmesEndPoints(this IEndpointRouteBuilder app) {

            app.MapGet("/filmes", FilmesHandlers.GetFilmes)
            .WithOpenApi();

            app.MapGet("/filmes/{id}", FilmesHandlers.GetFilmesById)
            .WithOpenApi();

            app.MapGet("/filmes/EFFunctions/byName{titulo}", FilmesHandlers.GetFilmesEFFunctionsByName)
            .WithOpenApi();

            app.MapGet("/filmes/Contains/byName{titulo}", FilmesHandlers.GetFilmesContainsByName)
            .WithOpenApi();

            app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme)
            .WithOpenApi();

            // Novo conceito do EF Core 8 - utilizando método ExecuteUpdate - Atualização parcial verbo HTTP - Patch - dados em massa
            app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.UpdateExecuteFilme)
            .WithOpenApi();

            // Novo conceito do EF.Core 8 - utilizando método ExecuteDelete - dados em massa
            app.MapDelete("/filmes/{filmeId}", FilmesHandlers.DeleteFilme)
            .WithOpenApi();
        }
    }
}
