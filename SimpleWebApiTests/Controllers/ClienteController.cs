using Microsoft.AspNetCore.Mvc;
using SimpleWebApiTests.Entities;
using SimpleWebApiTests.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SimpleWebApiTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteServico clienteServico, IClienteRepositorio clienteRepositorio)
        {
            _clienteServico = clienteServico;
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        [Route("ConsultarCliente/{cpfCnpj}")]
        public async Task<IActionResult> ConsultarClientePorCpf(string cpfCnpj)
        {
            try
            {
                var cliente = await _clienteRepositorio.ConsultarClientePorCpfCnpjAsync(cpfCnpj);

                return cliente is null ? Response(statusCode: HttpStatusCode.NoContent) : Response(cliente);
            }
            catch (Exception erro)
            {
                return Response(erro.Message, statusCode: HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("AlterarCliente")]
        public async Task<IActionResult> AlterarCliente([FromBody] Cliente cliente)
        {
            try
            {
                await _clienteServico.AdicionarClienteAsync(cliente);

                return Response(statusCode: HttpStatusCode.Accepted);
            }
            catch (Exception erro)
            {
                return Response(erro.Message, statusCode: HttpStatusCode.InternalServerError);
            }
        }

        private new IActionResult Response(object result = null, HttpStatusCode statusCode = HttpStatusCode.OK) => StatusCode((int)statusCode, result);
    }
}
