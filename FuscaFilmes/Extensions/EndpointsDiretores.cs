using FuscaFilmes.API.EndpointsHandlers;

namespace FuscaFilmes.API.Extensions {
    public static class EndpointsDiretores {

        public static void DiretoresEndPoints(this IEndpointRouteBuilder app) {

            app.MapGet("/diretores", DiretoresHandlers.GetDiretores)
            .WithOpenApi();

            app.MapGet("/diretores/aggregate/{name}", DiretoresHandlers.GetDiretoresByName)
            .WithOpenApi();

            app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretoresById)
            .WithOpenApi();

            app.MapPost("/diretores", DiretoresHandlers.AddDiretor)
            .WithOpenApi();

            app.MapPut("/diretores", DiretoresHandlers.UpdateDiretor)
            .WithOpenApi();

            app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretor)
            .WithOpenApi();

        }

        
    }
}
