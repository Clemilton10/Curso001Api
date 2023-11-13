using Curso001Api.Models;
using Curso001Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso001Api.Controllers
{
	[Authorize]
	[Route("tarefas")]
	[ApiController]
	public class TarefaController : ControllerBase
	{
		[HttpGet]
		//[AllowAnonymous]
		public async Task<IActionResult> Read([FromServices] ITarefaRepository repository)
		{
			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			var tarefas = await repository.Read(UsuarioId);
			return Ok(tarefas);
		}
		[HttpGet("{Id}")]
		//[AllowAnonymous]
		public async Task<IActionResult> OneRead(int Id, [FromServices] ITarefaRepository repository)
		{
			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			var tarefa = await repository.OneRead(UsuarioId, Id);
			return Ok(tarefa);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TarefaBase tarefa, [FromServices] ITarefaRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			Tarefa t = new Tarefa();
			t.Nome = tarefa.Nome;
			t.Concluida = tarefa.Concluida;
			t.UsuarioId = UsuarioId;
			await repository.Create(t);
			return Ok(t);
		}
		[HttpPut("{Id}")]
		public async Task<IActionResult> Update(int Id, [FromBody] TarefaBase tarefa, [FromServices] ITarefaRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			Tarefa t = new Tarefa();
			t.Id = Id;
			t.UsuarioId = UsuarioId;
			t.Nome = tarefa.Nome;
			t.Concluida = tarefa.Concluida;
			await repository.Update(t);
			return Ok(t);
		}
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(int Id, [FromServices] ITarefaRepository repository)
		{
			await repository.Delete(Id);
			return NoContent();
		}
	}
}