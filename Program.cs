using Curso001Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Abrindo o arquivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Pegando a string de conexao do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("PessoaDb");

// Configure o banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(connectionString));

// Registre o repositorio no container de injecao de dependencia
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

builder.Services.AddControllers();

// Pegando a string da key do appsettings.json
string sKey = builder.Configuration.GetConnectionString("key");
var key = Encoding.ASCII.GetBytes(sKey);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = false,
		ValidateAudience = false,
	};
});

// Configura o Swagger em ambiente de desenvolvimento
if (builder.Environment.IsDevelopment())
{
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curso API", Version = "v1" });

		var filePath = Path.Combine(System.AppContext.BaseDirectory, "Curso001Api.xml");
		c.IncludeXmlComments(filePath);

		// Authorization in swagger
		c.AddSecurityDefinition(
			"Bearer",
			new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please insert token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "bearer"
			}
		);
		c.AddSecurityRequirement(
			new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[]{ }
				}
			}
		);
	});
}

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria o banco de dados e aplica as migracoes na inicializacao
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();

	// Swagger na Home(so- na webapi)
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "CursoApi");
	});
	// Sem o Swagger na Home
	//app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// enable jwt security
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();

