using Dashboard_Times.Models;

namespace Dashboard_Times.Repository.Contract
{
    public interface IPosicaoRepository
    {
        IEnumerable<Posicao> ObterTodasPosicoes();
    }
}
