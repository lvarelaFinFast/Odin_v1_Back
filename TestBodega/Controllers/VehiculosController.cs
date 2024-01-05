using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio.IRepositorio;

namespace TestBodega.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVechiculosRepositorio _vehiculoRepositorio;

        public VehiculosController(IVechiculosRepositorio vechiculosRepositorio)
        {
            _vehiculoRepositorio = vechiculosRepositorio;
        }

        [HttpGet]
        [Route("GetVehiculos")]
        public async Task<IActionResult> GetVehiculos()
        {
            try
            {
                var result = await _vehiculoRepositorio.GetVehiculos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarVehiculo")]
        public async Task<IActionResult> GuardarVehiculo([FromBody] InputVehiculo input)
        {
            try
            {
                var result = await _vehiculoRepositorio.GuardarVehiculo(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPropietario")]
        public async ValueTask<IActionResult> GetPropietario(string valorBusqueda)
        {
            try
            {
                var result = await _vehiculoRepositorio.GetPropietario(valorBusqueda);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPropietarioById")]
        public async ValueTask<IActionResult> GetPropietarioById(int id)
        {
            try
            {
                var result = await _vehiculoRepositorio.GetPropietarioById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarPropietario")]
        public async ValueTask<IActionResult> GuardarPropietario([FromBody] InputPropietario input)
        {
            try
            {
                var result = await _vehiculoRepositorio.GuardarPropietario(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("AsignarPropietario")]
        public async ValueTask<IActionResult> AsignarPropietario([FromBody] InputAsignarPropietario input)
        {
            try
            {
                var result = await _vehiculoRepositorio.AsignarPropietario(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarEmpresa")]
        public async ValueTask<IActionResult> GuardarEmpresa([FromBody] InputEmpresa input)
        {
            try
            {
                var result = await _vehiculoRepositorio.GuardarEmpresa(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmpresa")]
        public async ValueTask<IActionResult> GetEmpresa(int idEmpresa)
        {
            try
            {
                var result = await _vehiculoRepositorio.GetEmpresa(idEmpresa);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmpresaByNit")]
        public async ValueTask<IActionResult> GetEmpresaByNit(string nit)
        {
            try
            {
                var result = await _vehiculoRepositorio.GetEmpresaByNit(nit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerArchivoVehiculos")]
        public async ValueTask<IActionResult> ObtenerArchivoVehiculos(string ruta)
        {
            try
            {
                var result = await _vehiculoRepositorio.ObtenerArchivoVehiculos(ruta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarVehiculo")]
        public async ValueTask<IActionResult> ActualizarVehiculo([FromBody] InputVehiculoUpdate input)
        {
            try
            {
                var result = await _vehiculoRepositorio.ActualizarVehiculo(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTipoArchivo")]
        public async ValueTask<IActionResult> GetTipoArchivo()
        {
            try
            {
                var result = await _vehiculoRepositorio.GetTipoArchivo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetArchivosVehiculos")]
        public async Task<IActionResult> GetArchivosVehiculos(int idVehiculo)
        {
            try
            {
                var archivos = await _vehiculoRepositorio.GetArchivos(idVehiculo);

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("ImportarArchivoVehiculo")]
        public async Task<IActionResult> ImportarArchivoVehiculo([FromBody] InputArchivosVehiculos input)
        {
            try
            {
                var result = await _vehiculoRepositorio.ImportarArchivo(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
