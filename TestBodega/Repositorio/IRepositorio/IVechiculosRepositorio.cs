using System.Collections.Generic;
using System.Threading.Tasks;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;

namespace TestBodega.Repositorio.IRepositorio
{
    public interface IVechiculosRepositorio
    {
        Task<List<VehiculoDTO>> GetVehiculos();
        Task<LlaveValorDTO> GuardarVehiculo(InputVehiculo input);
        ValueTask<List<PropietarioDTO>> GetPropietarios();
        ValueTask<List<PropietarioDTO>> GetPropietario(string valorBusqueda);
        ValueTask<LlaveValorObjectDTO<Propietario>> GuardarPropietario(InputPropietario input);
        ValueTask<LlaveValorDTO> AsignarPropietario(InputAsignarPropietario input);
        ValueTask<LlaveValorObjectDTO<EmpresaVinculante>> GuardarEmpresa(InputEmpresa input);
        ValueTask<EmpresaVinculanteDTO> GetEmpresa(int idEmpresa);
        ValueTask<EmpresaVinculanteDTO> GetEmpresaByNit(string nit);
        ValueTask<PropietarioDTO> GetPropietarioById(int id);
        ValueTask<LlaveValorDTO> ActualizarVehiculo(InputVehiculoUpdate input);
        ValueTask<List<TipoArchivoDTO>> GetTipoArchivo();
        Task<List<ArchivosVehiculosDTO>> GetArchivos(int idVehiculo);
        Task<LlaveValorDTO> ImportarArchivo(InputArchivosVehiculos input);
        ValueTask<FileDTO> ObtenerArchivoVehiculos(string ruta);

    }
}
