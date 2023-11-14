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
