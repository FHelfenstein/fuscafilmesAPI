using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.API.EndpointsHandlers {
    public static class DiretoresHandlers {

        public static async Task<IEnumerable<Diretor>> GetDiretoresAsync(IDiretorRepository diretorRepository) {
            return await  diretorRepository.GetDiretoresAsync();
        }

        public static async Task<Diretor> GetDiretoresByNameAsync(IDiretorRepository diretorRepository ,string name) {
            return await diretorRepository.GetDiretorByNameAsync(name);
            
        }

        public static async Task<IEnumerable<Diretor>> GetDiretoresByIdAsync(IDiretorRepository diretorRepository, int id) {

            return await diretorRepository.GetDiretoresByIdAsync(id);
        }

        public static async Task AddDiretorAsync(IDiretorRepository diretorRepository,  Diretor diretor) {
            await  diretorRepository.AddAsync(diretor);
            await diretorRepository.SaveChangesAsync();
        }

        public static async Task UpdateDiretorAsync(IDiretorRepository diretorRepository, Diretor diretorNovo) 
        {
            await diretorRepository.UpdateAsync(diretorNovo);
            await diretorRepository.SaveChangesAsync();
        }

        public static async Task DeleteDiretorAsync(IDiretorRepository diretorRepository, int diretorId) {
            await diretorRepository.DeleteAsync(diretorId);
            await diretorRepository.SaveChangesAsync();
        }
    }
}
