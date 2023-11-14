# Program.cs

Original

```csharp
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

# Swagger Comments

Curso001Api.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
	<PropertyGroup>
		<GenerateDocumentationFile>true</GenerateDocumentationFile>
	</PropertyGroup>
</Project>
```

Program.cs

```csharp
var filePath = Path.Combine(System.AppContext.BaseDirectory, "Curso001Api.xml");
c.IncludeXmlComments(filePath);
```

Controllers

```csharp
/// <summary>
/// Enter description for method AnotherMethod.
/// ID string generated is "M:MyNamespace.MyClass.AnotherMethod(System.Int16[],System.Int32[0:,0:])".
/// </summary>
/// <param name="array1">Describe parameter.</param>
/// <param name="array">Describe parameter.</param>
/// <returns>Describe return value.</returns>
[Route("usuarios")]
[ApiController]
```