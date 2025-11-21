using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using FuscaFilmes.API.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Aqui criar um banco de dados do zero
//using (var context = new Context()) 
//{ 
//    context.Database.EnsureCreated();
//};

// Adicionado configura��o para inje��o de depend�ncia do meu contexto
builder.Services.AddDbContext<Context>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"])
                      .LogTo(Console.WriteLine, LogLevel.Information)
    );

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configura��o para refer�ncia em loop
builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/diretores", (Context context) => {
    return context.Diretores
   .Include(x => x.Filmes)
   .ToList();
})
.WithOpenApi();

app.MapGet("/diretores/aggregate", (Context context) => {
    return context.Diretores
    .Include(x => x.Filmes)
    .Select(x => x.Name)
   .FirstOrDefault();
})
.WithOpenApi();


app.MapGet("/diretores/aggregate/{name}", (string name, Context context) => {

    // observar o tipo que está retornando para poder implementar o default , aqui estou retornando somente o campo do nome do diretor 
    // não é mais um objeto é somente retorno de string então o default precisa seguir a mesma tipagem
    return context.Diretores
    .Include(x => x.Filmes)
    .Select(x => x.Name)
    .FirstOrDefault()
    ?? "Maria das Graças";

    // return context.Diretores
    // .Include(x => x.Filmes)
    //.FirstOrDefault(x => x.Name.Contains(name))
    //// Quando não satisfaz a condição do First ai implementamos o Default aqui estamos criando um novo diretor 
    // ?? new Diretor { Id = 99999, Name = "Marina" };
})
.WithOpenApi();

app.MapGet("/diretores/where/{id}", (int id, Context context) => {
    return context.Diretores
    .Where(x => x.Id == id)
   .Include(x => x.Filmes)
   .ToList();
})
.WithOpenApi();


app.MapGet("/filmes", (Context context) => {
    return context.Filmes
   .Include(x => x.Diretor)
   .OrderByDescending(x => x.Ano)
   .ThenBy(x => x.Titulo)
   .ToList();
})
.WithOpenApi();


app.MapGet("/filmes/{id}", (int id, Context context) => {
    return context.Filmes
    .Where(x => x.Id == id)
   .Include(x => x.Diretor)
   .ToList();
})
.WithOpenApi();

app.MapGet("/filmes/EFFunctions/byName{titulo}", (string titulo, Context context) => {
    return context.Filmes
     .Where(x =>
        EF.Functions.Like(x.Titulo, $"%{titulo}%")
     )
    .Include(x => x.Diretor)
    .ToList();
})
.WithOpenApi();

app.MapGet("/filmes/LinQ/byName{titulo}", (string titulo, Context context) => {
    return context.Filmes
    .Where(x => x.Titulo.ToLower().Contains(titulo))
   .Include(x => x.Diretor)
   .ToList();
})
.WithOpenApi();

app.MapPost("/diretores", (Diretor diretor, Context context) => {
    context.Diretores.Add(diretor);
    context.SaveChanges();

})
.WithOpenApi();



app.MapPut("/diretores/{diretorId}", (int diretorId, Diretor diretorNovo, Context context) => {

    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null) {
        diretor.Name = diretorNovo.Name;
        if (diretorNovo.Filmes.Count > 0) {
            diretor.Filmes.Clear();
            foreach (var filme in diretorNovo.Filmes) {
                diretor.Filmes.Add(filme);
            }
        }

        context.SaveChanges();
    }
})
.WithOpenApi();

app.MapPatch("/filmesUpdate", (Context context, FilmeUpdate filmeUpdate) => {

    var filme = context.Filmes.Find(filmeUpdate.Id);

    if(filme == null) {
        return Results.NotFound("Filme não encontrado.");
    }

    filme.Titulo = filmeUpdate.Titulo;
    filme.Ano = filmeUpdate.Ano;

    context.Filmes.Update(filme);
    context.SaveChanges();

    return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com sucesso.");

})
.WithOpenApi();

// Novo conceito do Entity.Framework.Core - utilizando método ExecuteUpdate - Atualização parcial verbo HTTP - Patch
app.MapPatch("/filmesExecuteUpdate", (Context context, FilmeUpdate filmeUpdate) => {

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
})
.WithOpenApi();


// Novo conceito do Entity.Framework.Core - utilizando método ExecuteDelete
app.MapDelete("/filmes/{filmeId}", (int filmeId, Context context) => {

    context.Filmes
        .Where(x => x.Id == filmeId)
        .ExecuteDelete<Filme>();
})
.WithOpenApi();


app.MapDelete("/diretores/{diretorId}", (int diretorId, Context context) => {

    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
        context.Diretores.Remove(diretor);

    context.SaveChanges();
})
.WithOpenApi();



app.Run();

