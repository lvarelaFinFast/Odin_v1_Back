using AutoMapper;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;

namespace TestBodega.BodegaMappers
{
    public class BodegaMapper : Profile
    {
        public BodegaMapper()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();
            CreateMap<Conductores, ConductoresDTO>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDTO>().ReverseMap();
            CreateMap<Vehiculo, InputVehiculo>().ReverseMap();
            CreateMap<Propietario, PropietarioDTO>().ReverseMap();
            CreateMap<Propietario, InputPropietario>().ReverseMap();
            CreateMap<Propietario, InputAsignarPropietario>().ReverseMap();
            CreateMap<EmpresaVinculante, InputEmpresa>().ReverseMap();
            CreateMap<EmpresaVinculante, EmpresaVinculanteDTO>().ReverseMap();
            CreateMap<ArchivosVehiculos, ArchivosVehiculosDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Cliente, InputCliente>().ReverseMap();
            CreateMap<Contrato, ContratoDTO>().ReverseMap();
            CreateMap<Contrato, InputContrato>().ReverseMap();
            CreateMap<InputExtracto, InputActualizarExtracto>().ReverseMap();



        }
    }
}
