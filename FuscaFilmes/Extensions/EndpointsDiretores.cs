using FuscaFilmes.API.EndpointsHandlers;

namespace FuscaFilmes.API.Extensions {
    public static class EndpointsDiretores {

        public static void DiretoresEndPoints(this IEndpointRouteBuilder app) {

            app.MapGet("/diretores", DiretoresHandlers.GetDiretoresAsync)
            .WithOpenApi();

            app.MapGet("/diretores/aggregate/{name}", DiretoresHandlers.GetDiretoresByNameAsync)
            .WithOpenApi();

            app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretoresByIdAsync)
            .WithOpenApi();

            app.MapPost("/diretores", DiretoresHandlers.AddDiretorAsync)
            .WithOpenApi();

            app.MapPut("/diretores", DiretoresHandlers.UpdateDiretorAsync)
            .WithOpenApi();

            app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretorAsync)
            .WithOpenApi();

        }

        
    }
}
