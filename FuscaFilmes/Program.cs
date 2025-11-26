using FuscaFilmes.API.Extensions;
using FuscaFilmes.Repo;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Adicionado configura��o para inje��o de depend�ncia do meu contexto
builder.Services.AddDbContext<Context>(options => options
                        .UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"])
                        .LogTo(Console.WriteLine, LogLevel.Information));

// Adicionando a injeção para meu repositorio , principio SOLID , responsabilidades , a minha API está delegando a responsabilidade ao meu repositório
// A implementação está no meu repositório
builder.Services.AddScoped<IDiretorRepository, DiretorRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configura��o para refer�ncia em loop
builder.Services.Configure<JsonOptions>(options => 
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CLASSES EXTENDIDAS PARA DIRETORES E FILMES
app.DiretoresEndPoints();
app.FilmesEndPoints();

app.Run();

