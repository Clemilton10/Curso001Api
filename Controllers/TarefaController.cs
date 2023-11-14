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
		/// <summary>Busca todas as Tarefas</summary>
		/// <response code="200">Sucesso</response>
		/// <response code="404">Nao encontrado</response>
		[HttpGet]
		//[AllowAnonymous]
		[ProducesResponseType(typeof(List<Tarefa>), 200)]
		[ProducesResponseType(typeof(Directory), 404)]
		public async Task<IActionResult> Read([FromServices] ITarefaRepository repository)
		{
			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			var tarefas = await repository.Read(UsuarioId);
			if (tarefas == null || tarefas.Count() == 0)
			{
				return NotFound(new { });//404
			}
			return Ok(tarefas);//200
		}


		/// <summary>Busca uma Tarefa</summary>
		/// <param name="Id">Id do Tarefa</param>
		/// <response code="200">Ok - Sucesso</response>
		/// <response code="400">Bad Request - Requisicao invalida</response>
		/// <response code="404">Not Found - Nao encontrado</response>
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(Tarefa), 200)]
		[ProducesResponseType(typeof(Directory), 400)]
		[ProducesResponseType(typeof(Directory), 404)]
		public async Task<IActionResult> OneRead(int Id, [FromServices] ITarefaRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { });//400

			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			var tarefa = await repository.OneRead(UsuarioId, Id);

			if (tarefa == null)
				return NotFound(new { });//404

			return Ok(tarefa);
		}

		/// <summary>Adiciona uma Tarefa</summary>
		/// <response code="201">Ok - Criado</response>
		/// <response code="400">Bad Request - Requisicao invalida</response>
		[HttpPost]
		[ProducesResponseType(typeof(Tarefa), 201)]
		[ProducesResponseType(typeof(Directory), 400)]
		public async Task<IActionResult> Create([FromBody] TarefaBase tarefa, [FromServices] ITarefaRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { });//400

			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			Tarefa t = new Tarefa();
			t.Nome = tarefa.Nome;
			t.Concluida = tarefa.Concluida;
			t.UsuarioId = UsuarioId;
			await repository.Create(t);
			return CreatedAtAction("Get", new { id = t.Id }, t);//201
		}

		/// <summary>Atualiza uma Tarefa</summary>
		/// <param name="Id">Id da Tarefa</param>
		/// <response code="200">Ok - Sucesso</response>
		/// <response code="400">Bad Request - Requisicao invalida</response>
		[HttpPut("{Id}")]
		[ProducesResponseType(typeof(Tarefa), 200)]
		[ProducesResponseType(typeof(Directory), 400)]
		public async Task<IActionResult> Update(int Id, [FromBody] TarefaBase tarefa, [FromServices] ITarefaRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { });//400

			var UsuarioId = Convert.ToInt32(User.Identity.Name);
			Tarefa t = new Tarefa();
			t.Id = Id;
			t.UsuarioId = UsuarioId;
			t.Nome = tarefa.Nome;
			t.Concluida = tarefa.Concluida;
			await repository.Update(t);
			return Ok(t);//200
		}

		/// <summary>Exclui uma Tarefa</summary>
		/// <param name="Id">Id da Tarefa</param>
		/// <response code="204">No Content - Sem counteudo</response>
		[HttpDelete("{Id}")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> Delete(int Id, [FromServices] ITarefaRepository repository)
		{
			await repository.Delete(Id);
			return NoContent();//204
		}
	}
}