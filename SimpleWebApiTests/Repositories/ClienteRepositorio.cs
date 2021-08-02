using Dapper;
using SimpleWebApiTests.Entities;
using SimpleWebApiTests.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace SimpleWebApiTests.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IContextDapper _contextDapper;

        public ClienteRepositorio(IContextDapper contextDapper) => _contextDapper = contextDapper;

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            using var conn = _contextDapper.Connection();

            await conn.ExecuteAsync(ScriptSql.INSERIR_CLIENTE,
                param: new
                {
                    CpfCnpj = cliente.CpfCnpj,
                    Nome = cliente.Nome,
                    Mae = cliente.Mae,
                    Pai = cliente.Pai
                },
                commandType: CommandType.Text);
        }

        public async Task AlterarClienteAsync(Cliente cliente) 
        {
            using var conn = _contextDapper.Connection();

            await conn.ExecuteAsync(ScriptSql.ALTERAR_CLIENTE,
                param: new
                {
                    Nome = cliente.Nome,
                    Mae = cliente.Mae,
                    Pai = cliente.Pai,
                    CpfCnpj = cliente.CpfCnpj
                },
                commandType: CommandType.Text);
        }

        public async Task<Cliente> ConsultarClientePorCpfCnpjAsync(string cpfCnpj)
        {
            using var conn = _contextDapper.Connection();

            return await conn.QueryFirstOrDefaultAsync<Cliente>(ScriptSql.CONSULTAR_CLIENTE,
                param: new 
                {
                    CpfCnpj = cpfCnpj
                }, 
                commandType: CommandType.Text);
        }
    }
}
