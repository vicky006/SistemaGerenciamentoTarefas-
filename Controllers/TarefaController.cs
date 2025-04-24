using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id) //OK
        {
             // TODO: Buscar o Id no banco utilizando o EF -- IMPLEMENTADO
            var tarefa = _context.Tarefas.Find(id);

            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound -- IMPLEMENTADO
            if (tarefa == null)
            return NotFound();
           
          
            // caso contrário retornar OK com a tarefa encontrada -- IMPLEMENTADO
            return Ok(tarefa); //alterar o retorno se necessário
        }

        [HttpGet("ObterTodos")] // OK
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF - IMPLEMENTADO
            var tarefa= _context.Tarefas.ToList();

            if(tarefa == null)
            return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo")] // OK
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro - IMPLEMENTADO
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")] //OK
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro - JA ESTAVA IMPLEMENTADO
             var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
            // Dica: Usar como exemplo o endpoint ObterPorData
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa) // OK
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes) - IMPLEMENTADO
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")] // ok
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro - IMPLEMENTADO
                tarefaBanco.Titulo = tarefa.Titulo;
                tarefaBanco.Descricao = tarefa.Descricao;
                tarefaBanco.Data = tarefa.Data;
                tarefaBanco.Status = tarefa.Status;

            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes) - implementado
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")] // ok
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes) - IMPLEMENTADO
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
