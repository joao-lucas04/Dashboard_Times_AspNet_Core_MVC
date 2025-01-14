using Dashboard_Times.Models;

namespace Dashboard_Times.Repository.Contract
{
    public interface IEstatisticasJogadorRepository
    {
        EstatisticasJogador ObterEstatisticas(int Id);
        void AtualizarEstatisticas(EstatisticasJogador EstJogador);
    }
}
