using Moq;
using Moq.AutoMock;
using SimpleWebApiTests.Entities;
using SimpleWebApiTests.Interfaces;
using SimpleWebApiTests.Services;
using System.Threading.Tasks;
using Tests.Shared.Builders;
using Xunit;

namespace Tests.Unit.Services
{
    public class ClienteServicoTests
    {
        private readonly AutoMocker _mocker;
        private readonly ClienteServico _servico;
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;

        public ClienteServicoTests()
        {
            _mocker = new AutoMocker();
            _clienteRepositorio = _mocker.GetMock<IClienteRepositorio>();
            _servico = _mocker.CreateInstance<ClienteServico>();
        }

        [Fact(DisplayName = "NaoDeveInserirOuEditarCliente")]
        public async Task NaoDeveInserirOuEditarCliente()
        {
            await _servico.AdicionarClienteAsync(It.IsAny<Cliente>());

            _clienteRepositorio.Verify(x => x.AdicionarClienteAsync(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositorio.Verify(x => x.AlterarClienteAsync(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositorio.Verify(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>()), Times.Never);
        }
        
        [Fact(DisplayName = "DeveInserirUmCliente")]
        public async Task DeveInserirUmCliente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .Instanciar();

            await _servico.AdicionarClienteAsync(clienteBuilder);

            _clienteRepositorio.Verify(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>()), Times.Once);
            _clienteRepositorio.Verify(x => x.AdicionarClienteAsync(It.IsAny<Cliente>()), Times.Once);
            _clienteRepositorio.Verify(x => x.AlterarClienteAsync(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact(DisplayName = "DeveEditarUmCliente")]
        public async Task DeveEditarUmCliente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .Instanciar();

            var clienteBuilderAlterado = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .ComNome("Nome Cliente Alterado")
                .Instanciar();

            _clienteRepositorio.Setup(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>())).ReturnsAsync(clienteBuilder);

            await _servico.AdicionarClienteAsync(clienteBuilderAlterado);

            _clienteRepositorio.Verify(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>()), Times.Once);
            _clienteRepositorio.Verify(x => x.AdicionarClienteAsync(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositorio.Verify(x => x.AlterarClienteAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "NaoDeveEditarCliente")]
        public async Task DeveEditarCliente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .Instanciar();

            _clienteRepositorio.Setup(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>())).ReturnsAsync(clienteBuilder);

            await _servico.AdicionarClienteAsync(clienteBuilder);

            _clienteRepositorio.Verify(x => x.ConsultarClientePorCpfCnpjAsync(It.IsAny<string>()), Times.Once);
            _clienteRepositorio.Verify(x => x.AdicionarClienteAsync(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositorio.Verify(x => x.AlterarClienteAsync(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
