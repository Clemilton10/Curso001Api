using Curso001Api.Models;
using Curso001Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Curso001Api.Controllers
{

	[Route("usuarios")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public UsuarioController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Create([FromBody] UsuarioBase usuario, [FromServices] IUsuarioRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var u = new Usuario();
			u.Nome = usuario.Nome;
			u.Email = usuario.Email;
			u.Senha = usuario.Senha;
			await repository.Create(u);
			u.Senha = "******";
			return Ok(u);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] UsuarioLogin usuario, [FromServices] IUsuarioRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var u = new Usuario();
			u = await repository.Read(usuario.Email, usuario.Senha);
			if (u == null)
				return Unauthorized();

			u.Senha = "******";
			return Ok(
				new
				{
					usuario = u,
					token = GenerateToken(u)
				}
			);
		}

		private string GenerateToken(Usuario usuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration.GetSection("ConnectionStrings:Key").Value);
			var descriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(
					new Claim[]
					{
						new Claim(ClaimTypes.Name, usuario.Id.ToString())
					}
				),
				Expires = DateTime.UtcNow.AddHours(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
			};
			var token = tokenHandler.CreateToken(descriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}