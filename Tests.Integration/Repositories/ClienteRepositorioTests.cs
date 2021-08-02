using FluentAssertions;
using SimpleWebApiTests.Interfaces;
using System.Threading.Tasks;
using Tests.Integration.Tools;
using Tests.Shared.Builders;
using Xunit;

namespace Tests.Integration.Repositories
{
    public class ClienteRepositorioTests : IntegrationTestFixture
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteRepositorioTests(DatabaseFixture fixture) : base(fixture) => _clienteRepositorio = _fixture.GetService<IClienteRepositorio>();

        [Fact(DisplayName = "DeveInserirCliente")]
        public async Task DeveInserirCliente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .Instanciar();

            await _clienteRepositorio.AdicionarClienteAsync(clienteBuilder);

            var result = await _clienteRepositorio.ConsultarClientePorCpfCnpjAsync("12345678901");

            result.Should().NotBeNull();
            result.CpfCnpj.Should().Be("12345678901");
        }

        [Fact(DisplayName = "DeveAlterarUmCliente")]
        public async Task DeveAlterarUmCliente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .Instanciar();
            
            var clienteAlteradoBuilder = new ClienteBuilder()
                .ComCpfCnpj("12345678901")
                .ComNome("Nome Cliente Alterado")
                .Instanciar();

            await _clienteRepositorio.AdicionarClienteAsync(clienteBuilder);
            await _clienteRepositorio.AlterarClienteAsync(clienteAlteradoBuilder);

            var result = await _clienteRepositorio.ConsultarClientePorCpfCnpjAsync("12345678901");

            result.Should().NotBeNull();
            result.CpfCnpj.Should().Be("12345678901");
            result.Nome.Should().Be("Nome Cliente Alterado");
        }
    }
}
