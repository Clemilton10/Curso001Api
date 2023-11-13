using Curso001Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Curso001Api.Repositories
{
	public interface IUsuarioRepository
	{
		Task<Usuario> Read(string email, string senha);
		Task Create(Usuario usuario);
	}
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly AppDbContext _context;

		public UsuarioRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task Create(Usuario usuario)
		{
			//usuario.Id = Guid.NewGuid();
			_context.Usuarios.Add(usuario);
			await _context.SaveChangesAsync();
		}

		public async Task<Usuario> Read(string email, string senha)
		{
			return await _context.Usuarios.SingleOrDefaultAsync(
				usuario => usuario.Email == email && usuario.Senha == senha
			);
		}
	}
}