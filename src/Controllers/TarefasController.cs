using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Repositories;

namespace src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepo;
        public TarefasController(ITarefaRepository tarefaRepo)
        {
            _tarefaRepo = tarefaRepo;
        }

        [HttpGet]
        [Route("tarefas")]
        public async Task<IActionResult> GetTarefasAsync()
        {
            var result = await _tarefaRepo.GetTarefasAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("tarefa")]
        public async Task<IActionResult> GetTodoItemByIdAsync(int id)
        {
            var tarefa = await _tarefaRepo.GetTarefaByIdAsync(id);
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("tarefascontador")]
        public async Task<IActionResult> GetTodosAndCountAsync()
        {
            var resultado = await _tarefaRepo.GetTarefasEContadorAsync();
            return Ok(resultado);
        }

        [HttpPost]
        [Route("criartarefa")]
        public async Task<IActionResult> SaveAsync(tarefa novaTarefa)
        {
            var result = await _tarefaRepo.SaveAsync(novaTarefa);
            return Ok(result);
        }

        [HttpPost]
        [Route("atualizastatus")]
        public async Task<IActionResult> UpdateTodoStatusAsync(tarefa atualizaTarefa)
        {
            var result = await _tarefaRepo.UpdateTarefaStatusAsync(atualizaTarefa);
            return Ok(result);
        }

        [HttpDelete]
        [Route("deletatarefa")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _tarefaRepo.DeleteAsync(id);
            return Ok(result);
        }

    }
}