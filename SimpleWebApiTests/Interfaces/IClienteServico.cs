using SimpleWebApiTests.Entities;
using System.Threading.Tasks;

namespace SimpleWebApiTests.Interfaces
{
    public interface IClienteServico
    {
        Task AdicionarClienteAsync(Cliente cliente);
    }
}
