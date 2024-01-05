using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio;
using TestBodega.Repositorio.IRepositorio;

namespace TestBodega.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ConductoresController : Controller
    {
        private readonly IConductoresRepositorio _ConductoresRepo;

        public ConductoresController(IConductoresRepositorio ConductoresRepo)
        {
            _ConductoresRepo = ConductoresRepo;
        }
        [HttpGet]
        [Route("GetConductores")]
        public async Task<ActionResult<PaginatedList<ConductoresDTO>>> GetConductores([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var conductores = await _ConductoresRepo.GetConductores(pageNumber, pageSize);

                return Ok(conductores);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetConductoresWhitOutPaginator")]
        public async Task<ActionResult<List<ConductoresDTO>>> GetConductoresWhitOutPaginator()
        {
            try
            {
                var conductores = await _ConductoresRepo.GetConductoresWhitOutPaginator();

                return Ok(conductores);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetArchivos")]
        public async Task<IActionResult> GetArchivos(int idConductor)
        {
            try
            {
                var archivos = await _ConductoresRepo.GetArchivos(idConductor);

                return Ok(archivos);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarConductor")]
        public async Task<IActionResult> GuardarConductor([FromBody] InputConductor input)
        {
            try
            {
                var result = await _ConductoresRepo.GuardarConductor(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarConductor")]
        public async Task<IActionResult> ActualizarConductor([FromBody] InputConductor input)
        {
            try
            {
                var result = await _ConductoresRepo.ActualizarConductor(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTipoArchivos")]
        public async Task<IActionResult> GetTipoArchivos()
        {
            try
            {
                var result = await _ConductoresRepo.GetTipoArchivos();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("ImportarArchivo")]
        public async Task<IActionResult> ImportarArchivo([FromBody] InputArchivos input)
        {
            try
            {
                var result = await _ConductoresRepo.ImportarArchivo(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerArchivo")]
        public async ValueTask<IActionResult> ObtenerArchivo(string ruta)
        {
            try
            {
                var result = await _ConductoresRepo.ObtenerArchivo(ruta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GenerarReporte")]
        public async Task<IActionResult> GenerarReporte(int idExtracto)
        {
            try
            {
                var result = await _ConductoresRepo.GenerarArchivo(idExtracto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
    
}
