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
builder.Services.AddSwaggerGen(
	c => {
		var filePath = Path.Combine(System.AppContext.BaseDirectory, "Curso001Api.xml");
		c.IncludeXmlComments(filePath);
	}
);
```

Controllers

```csharp
namespace Api.Controllers
{
	[Authorize]
	[Route("books")]
	[ApiController]
	public class TarefaController : ControllerBase
	{
		/// <summary>Enter description for method AnotherMethod.</summary>
		/// <param name="array1">Describe parameter.</param>
		/// <param name="array">Describe parameter.</param>
		/// <returns>Describe return value.</returns>
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(Book), 200)]
		[ProducesResponseType(typeof(Directory), 400)]
		[ProducesResponseType(typeof(Directory), 404)]
		[AllowAnonymous]
```

# Responses

| Código |           -           | Descrição                                                                                                                                                                                                  |
| -----: | :-------------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|    200 |          OK           | Indica que a solicitação foi bem-sucedida. O servidor retornará essa resposta quando a operação foi concluída com êxito.                                                                                   |
|    201 |        Created        | Indica que a solicitação foi bem-sucedida e resultou na criação de um novo recurso. Este código de status é frequentemente usado após a criação de um recurso por meio de uma solicitação POST.            |
|    204 |      No Content       | Indica que a solicitação foi bem-sucedida, mas não há conteúdo a ser retornado. Isso é comum em operações que não retornam dados (por exemplo, uma exclusão bem-sucedida).                                 |
|    400 |      Bad Request      | Indica que a solicitação do cliente é inválida, malformada ou contém parâmetros inválidos. Este código é usado quando o servidor não pode ou não processará a solicitação por causa de um erro do cliente. |
|    401 |     Unauthorized      | Indica que a solicitação não foi autorizada. Pode ser necessário fornecer credenciais válidas para acessar os recursos protegidos.                                                                         |
|    403 |       Forbidden       | Indica que o servidor entende a solicitação, mas o servidor se recusa a autorizá-la. Diferente do 401, a autenticação não fará diferença.                                                                  |
|    404 |       Not Found       | Indica que o recurso solicitado não foi encontrado no servidor.                                                                                                                                            |
|    405 |  Method Not Allowed   | Indica que o método de solicitação (GET, POST, PUT, DELETE, etc.) não é permitido para o recurso solicitado.                                                                                               |
|    500 | Internal Server Error | Indica que ocorreu um erro interno no servidor ao processar a solicitação. Este é um código genérico para indicar falhas do servidor.                                                                      |
|    503 |  Service Unavailable  | Indica que o servidor não está pronto para lidar com a solicitação. Isso pode acontecer devido a sobrecarga do servidor ou manutenção temporária.                                                          |

```csharp
/// <response code="200">Ok - Sucesso</response>
/// <response code="201">Ok - Criado</response>
/// <response code="204">No Content - Sem counteudo</response>
/// <response code="400">Bad Request - Requisicao invalida</response>
/// <response code="401">Unauthorized - Nao autorizado</response>
/// <response code="403">Forbidden - Totalmente proibido</response>
/// <response code="404">Not Found - Nao encontrado</response>
/// <response code="405">Method Not Allowed - Metodo nao permitido</response>
/// <response code="500">Internal Server Error - Erro interno de servidor</response>
/// <response code="503">Service Unavailable - Servidor ocupado</response>
```

| Params       | Data                             |
| :----------- | :------------------------------- |
| path param   | api/books/{param}                |
| query param  | api/books?param={param}          |
| body param   | {"name":"Clemas"}                |
| header param | {"Authentication": "Bearer ..."} |

| Responses       | Response |
| :-------------- | :------- |
| 200 Ok          | Objeto   |
| 201 Created     | Objeto   |
| 204 No Content  | Nada     |
| 400 Bad Request | Nada     |
| 404 Not Found   | Nada     |

|  -  | Action |     Verb     | URL            |
| :-: | :----: | :----------: | :------------- |
|  C  | Create |     POST     | api/books      |
|  R  |  Read  |     GET      | api/books      |
|  U  | Update | PUT ou PATCH | api/books/{id} |
|  D  | Delete |    DELETE    | api/books/{id} |

|  Verb   | Action  | Responses                               | Params                    |
| :-----: | :-----: | :-------------------------------------- | :------------------------ |
|   GET   |  Read   | 200 Ok, 400 Bad Request, 404 Not Found  | path, query, header       |
|  POST   | Create  | 200 Ok, 201 Created, 400 Bad Request    | path, query, header, body |
|   PUT   | Update  | 200 Ok, 204 No Content, 400 Bad Request | path, query, header, body |
|  PATH   | Update  | 200 Ok, 204 No Content, 400 Bad Request | path, query, header, body |
| DELETE  | Delete  | 200 Ok, 204 No Content, 400 Bad Request | path, query, header, body |
|  HEAD   |  Head   | -                                       | -                         |
|  TRACE  |  Trace  | -                                       | -                         |
| OPTIONS | Options | -                                       | -                         |
| CONNECT | Connect | -                                       | -                         |

# REST and RESTfull - Níveis de maturidade de Richardson

| Levels  | -                   | Description                                         |
| :------ | :------------------ | :-------------------------------------------------- |
| Level 3 | Hypermedia Controls | Links com todas as operações possíveis com o objeto |
| Level 2 | HTTP Verbs          | GET, POST, PUT, DELETE                              |
| Level 1 | Resources           | tabelas separadas                                   |
| Level 0 | the swamp of pox    | http protocol, tudo misturado e desorganizado       |

```json
[
  {
    "id": 1,
    "firstName": "Leandro",
    "lastName": "Costa",
    "address": "Uberlândia - Minas Gerais - Brasil",
    "gender": "Male",
    "links": [
      {
        "rel": "self",
        "href": "http://localhost:50904/api/persons/v1/1",
        "type": "application/json",
        "action": "GET"
      },
      {
        "rel": "self",
        "href": "http://localhost:50904/api/persons/v1/1",
        "type": "application/x-www-form-urlencoded",
        "action": "POST"
      },
      {
        "rel": "self",
        "href": "http://localhost:50904/api/persons/v1/1",
        "type": "application/x-www-form-urlencoded",
        "action": "PUT"
      },
      {
        "rel": "self",
        "href": "http://localhost:50904/api/persons/v1/1",
        "type": "int",
        "action": "DELETE"
      }
    ]
  }
]
```
