using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.API.EndpointsHandlers {
    public static class DiretoresHandlers {

        public static IEnumerable<Diretor> GetDiretores(IDiretorRepository diretorRepository) {
            return diretorRepository.GetDiretores();
        }

        public static Diretor GetDiretoresByName(IDiretorRepository diretorRepository ,string name) {
            return diretorRepository.GetDiretorByName(name);
            
        }

        public static IEnumerable<Diretor> GetDiretoresById(IDiretorRepository diretorRepository, int id) {

            return diretorRepository.GetDiretoresById(id);
        }

        public static void AddDiretor(IDiretorRepository diretorRepository,  Diretor diretor) {
            diretorRepository.Add(diretor);
            diretorRepository.SaveChanges();
        }

        public static void UpdateDiretor(IDiretorRepository diretorRepository, Diretor diretorNovo) 
        {
            diretorRepository.Update(diretorNovo);
            diretorRepository.SaveChanges();
        }

        public static void DeleteDiretor(IDiretorRepository diretorRepository, int diretorId) {
            diretorRepository.Delete(diretorId);
            diretorRepository.SaveChanges();
        }
    }
}
