using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Repositorio.IRepositorio;

namespace TestBodega.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio _ProdRepo;
        private readonly IMapper _mapper;

        public ProductosController(IProductoRepositorio ProdRepo, IMapper mapper)
        {
            _ProdRepo = ProdRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("Productos")]
        public IActionResult GetProductos()
        {
            try
            {
                var ListProductos = _ProdRepo.GetProductos();

                var ListProductosDto = new List<ProductoDto>();

                foreach (var oProducto in ListProductos)
                {
                    ListProductosDto.Add(_mapper.Map<ProductoDto>(oProducto));
                }

                return Ok(ListProductosDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("ProductosDefectuosos")]
        public IActionResult GetProductosDefectuosos()
        {
            try
            {
                var ListProductos = _ProdRepo.GetProductosDefectuosos();

                var ListProductosDto = new List<ProductoDto>();

                foreach (var oProducto in ListProductos)
                {
                    ListProductosDto.Add(_mapper.Map<ProductoDto>(oProducto));
                }

                return Ok(ListProductosDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("ProductosOptimos")]
        public IActionResult GetProductosOptimos()
        {
            try
            {
                var ListProductos = _ProdRepo.GetProductosOptimos();

                var ListProductosDto = new List<ProductoDto>();

                foreach (var oProducto in ListProductos)
                {
                    ListProductosDto.Add(_mapper.Map<ProductoDto>(oProducto));
                }

                return Ok(ListProductosDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Route("GuardarProducto")]
        public IActionResult GuardarProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var oProducto = _mapper.Map<Producto>(crearProductoDto);
                var producto = _ProdRepo.CrearProducto(oProducto);

                return Ok(true);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        [HttpPatch]
        [Route("MarcarProductoOptimo")]
        public IActionResult MarcarProductoOptimo([FromBody] ProductoDto productoDto)
        {
            try
            {
               
                if (productoDto == null)
                {
                     return BadRequest(ModelState);
                }
                var producto = _mapper.Map<Producto>(productoDto);
                if (!_ProdRepo.MarcarOptimo(producto))
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el producto con codigo: {producto.CodigoProducto}");
                    return StatusCode(404, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [Route("MarcarProductoDefectuoso")]
        public IActionResult MarcarProductoDefectuoso([FromBody] ProductoDto productoDto)
        {
            try
            {
                if (productoDto == null)
                {
                    return BadRequest(ModelState);
                }
                var producto = _mapper.Map<Producto>(productoDto);
                if (!_ProdRepo.MarcarDefectuoso(producto))
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el producto con codigo: {producto.CodigoProducto}");
                    return StatusCode(404, ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        #region Usuario

        [HttpGet]
        [Route("GetUserList")]
        public IActionResult GetUserList()
        {
            try
            {
                var oReturn = _ProdRepo.GetUserList();

                return Ok(oReturn);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetUserAuth")]
        public IActionResult GetUserAuth([FromBody] Usuario usuario)
        {
            try
            {

                var user = _ProdRepo.GetUserAuth(usuario);

                return Ok(user);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("UspertUser")]
        public IActionResult UspertUser([FromBody] Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = _ProdRepo.UpsertUsuario(usuario);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

    }
}
