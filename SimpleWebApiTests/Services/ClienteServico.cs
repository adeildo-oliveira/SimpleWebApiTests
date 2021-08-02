using SimpleWebApiTests.Entities;
using SimpleWebApiTests.Interfaces;
using System.Threading.Tasks;

namespace SimpleWebApiTests.Services
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio) => _clienteRepositorio = clienteRepositorio;

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            if (cliente is null)
                return;

            var resultado = await _clienteRepositorio.ConsultarClientePorCpfCnpjAsync(cliente.CpfCnpj);

            if(resultado is null)
            {
                await _clienteRepositorio.AdicionarClienteAsync(cliente);
            }
            else
            {
                if(!new ClienteAlterado().Equals(cliente, resultado))
                {
                    await _clienteRepositorio.AlterarClienteAsync(cliente);
                }
            }
        }
    }
}
