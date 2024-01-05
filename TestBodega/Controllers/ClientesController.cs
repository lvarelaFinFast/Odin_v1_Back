using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio;
using TestBodega.Repositorio.IRepositorio;

namespace TestBodega.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("GetClientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var result = await _clienteRepository.GetClientes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarCliente")]
        public async Task<IActionResult> GuardarCliente([FromBody] InputCliente input)
        {
            try
            {
                var result = await _clienteRepository.GuardarCliente(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente([FromBody] InputActualizarCliente input)
        {
            try
            {
                var result = await _clienteRepository.ActualizarCliente(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarCliente")]
        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            try
            {
                var result = await _clienteRepository.EliminarCliente(idCliente);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
