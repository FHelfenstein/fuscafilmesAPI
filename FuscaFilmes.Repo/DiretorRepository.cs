using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo;

public class DiretorRepository(Context _context) : IDiretorRepository {

    public Context Context { get; } = _context;


    public async Task<IEnumerable<Diretor>> GetDiretoresAsync() {

        return await Context.Diretores
            .Include(x => x.Filmes)
            .ToListAsync();
    }

    public async Task<Diretor> GetDiretorByNameAsync(string name) {

        return await Context.Diretores
            .Include(x => x.Filmes)
            .FirstOrDefaultAsync(x => x.Name.Contains(name))
            ?? new Diretor { Id = 99999, Name = "INEXISTENTE" }; // Quando não satisfaz a condição do First ai implementamos o Default aqui estamos criando um novo diretor 
    }

    public async Task<IEnumerable<Diretor>> GetDiretoresByIdAsync(int id) {

        return await Context.Diretores.Where(x => x.Id == id)
            .Include(x => x.Filmes)
            .ToListAsync();
    }

    public async Task AddAsync(Diretor diretor) {
        await Context.Diretores.AddAsync(diretor);
    }

    public async Task DeleteAsync(int diretorId) {

        var diretor = await  Context.Diretores.FindAsync(diretorId);

        if (diretor != null)
            Context.Diretores.Remove(diretor);
    }

    public async Task UpdateAsync(Diretor diretorNovo) {

        var diretor = await  Context.Diretores.FindAsync(diretorNovo.Id);

        if (diretor != null) {
            diretor.Name = diretorNovo.Name;
            if (diretorNovo.Filmes.Count > 0) {
                diretor.Filmes.Clear();
                foreach (var filme in diretorNovo.Filmes) {
                    diretor.Filmes.Add(filme);
                }
            }
        }
    }

    public async Task<bool> SaveChangesAsync() {
        return await  Context.SaveChangesAsync() > 0;

    }

}