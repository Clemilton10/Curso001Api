using System.ComponentModel.DataAnnotations;

namespace Curso001Api.Models
{
	public class Tarefa
	{
		public int Id { get; set; }
		public int UsuarioId { get; set; } = 0;
		[Required]
		public string Nome { get; set; } = string.Empty;
		public int Concluida { get; set; } = 0;
	}
	public class TarefaBase
	{
		[Required]
		public string Nome { get; set; } = string.Empty;
		public int Concluida { get; set; } = 0;
	}
}