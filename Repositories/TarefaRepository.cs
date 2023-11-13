using Curso001Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Curso001Api.Repositories
{
	public interface ITarefaRepository
	{
		Task<Tarefa> OneRead(int UsuarioId, int Id);
		Task<IEnumerable<Tarefa>> Read(int UsuarioId);
		Task Create(Tarefa tarefa);
		Task Delete(int Id);
		Task Update(Tarefa tarefa);
	}
	public class TarefaRepository : ITarefaRepository
	{
		private readonly AppDbContext _context;
		public TarefaRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<Tarefa> OneRead(int UsuarioId, int Id)
		{
			//return await _context.Tarefas.FindAsync(Id);
			Tarefa tarefa = await _context.Tarefas
				.Where(tarefa => tarefa.UsuarioId == UsuarioId && tarefa.Id == Id)
				.FirstOrDefaultAsync();

			if (tarefa != null)
			{
				return tarefa;
			}
			else
			{
				return new Tarefa();
			}
		}
		public async Task<IEnumerable<Tarefa>> Read(int UsuarioId)
		{
			return await _context.Tarefas
				.Where(tarefa => tarefa.UsuarioId == UsuarioId)
				.ToListAsync();
		}
		public async Task Create(Tarefa tarefa)
		{
			//tarefa.Id = Guid.NewGuid();
			_context.Tarefas.Add(tarefa);
			await _context.SaveChangesAsync();
		}
		public async Task Delete(int Id)
		{
			var tarefa = _context.Tarefas.Find(Id);
			_context.Entry(tarefa).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
		}
		public async Task Update(Tarefa tarefa)
		{
			var _tarefa = _context.Tarefas.Find(tarefa.Id);
			_tarefa.Nome = tarefa.Nome;
			_tarefa.Concluida = tarefa.Concluida;
			_context.Entry(_tarefa).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}