# Program.cs

Original

```c#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

# Instalação

```sh
dotnet new webapi -n Curso001Api -f net6.0
cd Curso001Api
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.24
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.24
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.24

# Listar pacotes
dotnet list package

# instalar pacotes pelo XML
dotnet restore

# Remover pacotes
dotnet remove package nome

# Limpar
dotnet clean

# Executar
dotnet watch run
```