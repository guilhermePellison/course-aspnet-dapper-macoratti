using src.Data;

namespace src.Repositories
{
    public interface ITarefaRepository
    {
        Task<List<tarefa>> GetTarefasAsync();
        Task<tarefa> GetTarefaByIdAsync(int id);
        Task<tarefaContainer> GetTarefasEContadorAsync();
        Task<int> SaveAsync(tarefa novaTarefa);
        Task<int> UpdateTarefaStatusAsync(tarefa atualizaTarefa);
        Task<int> DeleteAsync(int id);
    }
}