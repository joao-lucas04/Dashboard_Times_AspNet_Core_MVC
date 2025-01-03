using Dashboard_Times.Models;

namespace Dashboard_Times.Repository.Contract
{
    public interface ITimeRepository
    {
        IEnumerable<Time> ObterTodosTimes();
        Time ObterTime(int Id);
        void AtualizarTime(Time time);
        void CadastrarTime(Time time);
        void DeletarTime(int Id);
        IEnumerable<Time> BuscarTime(string termo);
    }
}
