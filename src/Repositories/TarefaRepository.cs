using Dapper;
using src.Data;

namespace src.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private DbSession _db;
        public TarefaRepository(DbSession dbSession)
        {
            _db = dbSession;
        }
        public async Task<List<tarefa>> GetTarefasAsync()
        {
            using (var conn = _db.Connection)
            {
                string query = "SELECT * FROM tarefas";
                List<tarefa> tarefas = (await conn.QueryAsync<tarefa>(sql: query)).ToList();
                return tarefas;
            }
        }

        public async Task<tarefa> GetTarefaByIdAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = "SELECT * FROM tarefas WHERE Id = @id";
                tarefa tarefa = await conn.QueryFirstOrDefaultAsync<tarefa>
                    (sql: query, param: new { id });
                return tarefa;
            }
        }

        public async Task<tarefaContainer> GetTarefasEContadorAsync()
        {
            using (var conn = _db.Connection)
            {
                string query =
                    @"SELECT COUNT(*) FROM tarefas;
                    SELECT * FROM tarefas";

                var reader = await conn.QueryMultipleAsync(sql: query);
                return new tarefaContainer
                {
                    contador = (await reader.ReadAsync<int>()).FirstOrDefault(),
                    Tarefas = (await reader.ReadAsync<tarefa>()).ToList()
                };
            }
        }

        public async Task<int> SaveAsync(tarefa novaTarefa)
        {
            using (var conn = _db.Connection)
            {
                string command = @"INSERT INTO tarefas(Id, Descricao, IsCompleta) VALUES(@Id, @Descricao, @IsCompleta)";

                var result = await conn.ExecuteAsync(sql: command, param: novaTarefa);
                return result;
            }
        }
        public async Task<int> UpdateTarefaStatusAsync(tarefa atualizaTarefa)
        {
            using (var conn = _db.Connection)
            {
                string command = @"
                UPDATE Tarefas SET IsCompleta = @IsCompleta WHERE Id = @Id";

                var result = await conn.ExecuteAsync(sql: command, param: atualizaTarefa);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string command = @"DELETE FROM Tarefas WHERE Id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
        }
    }
}