using SimpleWebApiTests.Entities;
using System.Threading.Tasks;

namespace SimpleWebApiTests.Interfaces
{
    public interface IClienteRepositorio
    {
        Task AdicionarClienteAsync(Cliente cliente);
        Task AlterarClienteAsync(Cliente cliente);
        Task<Cliente> ConsultarClientePorCpfCnpjAsync(string cpfCnpj);
    }
}
