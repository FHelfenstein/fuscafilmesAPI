using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contexts;
using FuscaFilmes.Repo.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo;

public class DiretorRepository(Context _context) : IDiretorRepository {

    public Context Context { get; } = _context;


    public IEnumerable<Diretor> GetDiretores() {

        return Context.Diretores.Include(x => x.Filmes).ToList();
    }

    public Diretor GetDiretorByName(string name) {

        return Context.Diretores.Include(x => x.Filmes).FirstOrDefault(x => x.Name.Contains(name))
            ?? new Diretor { Id = 99999, Name = "INEXISTENTE" }; // Quando não satisfaz a condição do First ai implementamos o Default aqui estamos criando um novo diretor 
    }

    public IEnumerable<Diretor> GetDiretoresById(int id) {

        return Context.Diretores.Where(x => x.Id == id).Include(x => x.Filmes).ToList();
    }

    public void Add(Diretor diretor) {
        Context.Diretores.Add(diretor);
        Context.SaveChanges();

    }

    public void Delete(int diretorId) {

        var diretor = Context.Diretores.Find(diretorId);

        if (diretor != null)
            Context.Diretores.Remove(diretor);
    }

    public void Update(Diretor diretorNovo) {

        var diretor = Context.Diretores.Find(diretorNovo.Id);

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

    public bool SaveChanges() {
        return Context.SaveChanges() > 0;

    }
}