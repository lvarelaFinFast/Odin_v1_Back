using System.Collections.Generic;
using System.Threading.Tasks;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;

namespace TestBodega.Repositorio.IRepositorio
{
    public interface IConductoresRepositorio
    {
        // Conductores
        Task<PaginatedList<ConductoresDTO>> GetConductores(int pageNumber, int pageSize);
        Task<LlaveValorDTO> GuardarConductor(InputConductor input);
        Task<LlaveValorDTO> ActualizarConductor(InputConductor input);
        Task<LlaveValorDTO> DeleteConductor(int Idconductor);

        // Archivos
        Task<List<ArchivosDTO>> GetArchivos(int Idconductor);
        ValueTask<FileDTO> ObtenerArchivo(string ruta);
        Task<List<ConductoresDTO>> GetConductoresWhitOutPaginator();
        Task<List<TipoArchivoDTO>> GetTipoArchivos();
        Task<LlaveValorDTO> ImportarArchivo(InputArchivos input);
        Task<LlaveValorDTO> GenerarArchivo(int idExtracto);

    }
}
