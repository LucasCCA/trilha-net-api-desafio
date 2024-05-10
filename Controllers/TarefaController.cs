using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.DTO;
using TrilhaApiDesafio.Models.Enums;

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
        public IActionResult ObterPorId(int id)
        {
            List<Tarefa> tarefa = _context.Tarefas.Where(x => x.Id.Equals(id)).ToList();

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            List<Tarefa> tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            List<Tarefa> tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            List<Tarefa> tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            List<Tarefa> tarefa = _context.Tarefas.Where(x => x.Status == status).ToList();

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar([FromBody][Required] CreateTarefaDTO tarefaDto)
        {
            if (tarefaDto.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            Tarefa tarefa = new Tarefa()
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                Data = tarefaDto.Data,
                Status = tarefaDto.Status
            };

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody][Required] CreateTarefaDTO tarefaDto)
        {
            Tarefa tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            tarefa.Titulo = tarefaDto.Titulo;
            tarefa.Descricao = tarefaDto.Descricao;
            tarefa.Data = tarefaDto.Data;
            tarefa.Status = tarefaDto.Status;

            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            Tarefa tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
