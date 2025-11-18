namespace FuscaFilmes.API.Entities {

    // classe diretor
    public class Diretor {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Filme> Filmes { get; set; } = [];
    }
}
