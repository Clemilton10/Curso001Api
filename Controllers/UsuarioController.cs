using Curso001Api.Models;
using Curso001Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Curso001Api.Controllers
{
	[Authorize]
	[Route("usuarios")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public UsuarioController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>Adiciona um Usuario</summary>
		/// <response code="200">Ok - Sucesso</response>
		/// <response code="400">Bad Request - Requisicao invalida</response>
		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(Usuario), 201)]
		[ProducesResponseType(typeof(Directory), 400)]
		public async Task<IActionResult> Create([FromBody] UsuarioBase usuario, [FromServices] IUsuarioRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { });

			var u = new Usuario();
			u.Nome = usuario.Nome;
			u.Email = usuario.Email;
			u.Senha = usuario.Senha;
			await repository.Create(u);
			u.Senha = "******";
			return CreatedAtAction("Get", new { id = u.Id }, u);//201
		}

		/// <summary>Faz login</summary>
		/// <response code="200">Ok - Sucesso</response>
		/// <response code="400">Bad Request - Requisicao invalida</response>
		/// <response code="401">Unauthorized - Nao autorizado</response>
		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(UsuarioLoged), 200)]
		[ProducesResponseType(typeof(Directory), 400)]
		[ProducesResponseType(typeof(Directory), 401)]
		public async Task<IActionResult> Login([FromBody] UsuarioLogin usuario, [FromServices] IUsuarioRepository repository)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { });

			var u = new Usuario();
			u = await repository.Read(usuario.Email, usuario.Senha);
			if (u == null)
				return Unauthorized(new { });

			u.Senha = "******";
			UsuarioLoged un = new UsuarioLoged();
			un.usuario = u;
			un.token = GenerateToken(u);
			return Ok(un);
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