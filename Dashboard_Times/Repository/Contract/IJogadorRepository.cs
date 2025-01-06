using Dashboard_Times.Models;

namespace Dashboard_Times.Repository.Contract
{
    public interface IJogadorRepository
    {
        IEnumerable<Jogador> ObterTodosJogadores();
        Jogador ObterJogador(int Id);
        void AtualizarJogador(Jogador jogador);
        void CadastrarJogador(Jogador jogador);
        void DeletarJogador(int Id);
        IEnumerable<Jogador> BuscarJogador(string termo);
    }
}
