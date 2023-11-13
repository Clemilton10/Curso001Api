using Curso001Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Curso001Api.Repositories
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Tarefa> Tarefas { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }

	}
}