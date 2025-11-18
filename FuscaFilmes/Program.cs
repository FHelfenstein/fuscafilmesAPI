using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Aqui criar um banco de dados do zero
//using (var context = new Context()) 
//{ 
//    context.Database.EnsureCreated();
//};

// Adicionado configuração para injeção de dependência do meu contexto
builder.Services.AddDbContext<Context>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuração para referência em loop
builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/diretores", (Context context) =>
{
     return context.Diretores
    .Include(x => x.Filmes)
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

    if(diretor != null) {
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


app.MapDelete("/diretores/{diretorId}", (int diretorId, Context context) => {

    var diretor = context.Diretores.Find(diretorId);

    if(diretor != null)
        context.Diretores.Remove(diretor);

    context.SaveChanges();
})
.WithOpenApi();

app.Run();

