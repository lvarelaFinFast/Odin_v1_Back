using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio.IRepositorio;
using TestBodega.Repositorio;

namespace TestBodega.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ContratosController : Controller
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratosController(IContratoRepository contratoRepository)
        {
            _contratoRepository = contratoRepository;
        }

        [HttpGet]
        [Route("GetContratos")]
        public async Task<IActionResult> GetContratos()
        {
            try
            {
                var result = await _contratoRepository.GetContratos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarContrato")]
        public async Task<IActionResult> GuardarContrato([FromBody] InputContrato input)
        {
            try
            {
                var result = await _contratoRepository.GuardarContrato(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarContrato")]
        public async Task<IActionResult> ActualizarContrato([FromBody] InputActualizarContrato input)
        {
            try
            {
                var result = await _contratoRepository.ActualizarContrato(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarContrato")]
        public async Task<IActionResult> EliminarContrato(int idCliente)
        {
            try
            {
                var result = await _contratoRepository.EliminarContrato(idCliente);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMunicipios")]
        public async Task<IActionResult> GetMunicipios()
        {
            try
            {
                var result = await _contratoRepository.GetMunicipios();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetExtractos")]
        public async Task<IActionResult> GetExtractos()
        {
            try
            {
                var result = await _contratoRepository.GetExtractos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("CrearExtracto")]
        public async Task<IActionResult> CrearExtracto([FromBody] InputExtracto input)
        {
            try
            {
                var result = await _contratoRepository.CrearExtracto(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarExtracto")]
        public async Task<IActionResult> ActualizarExtracto([FromBody] InputActualizarExtracto input)
        {
            try
            {
                var result = await _contratoRepository.ActualizarExtracto(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
