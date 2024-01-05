using System.Collections.Generic;
using System.Threading.Tasks;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using TestBodega.Models;

namespace TestBodega.Repositorio
{
    public interface IContratoRepository
    {
        ValueTask<List<ContratoDTO>> GetContratos();
        ValueTask<LlaveValorDTO> GuardarContrato(InputContrato input);
        ValueTask<LlaveValorDTO> ActualizarContrato(InputActualizarContrato input);
        ValueTask<LlaveValorDTO> EliminarContrato(int idCliente);
        ValueTask<List<ExtractoDTO>> GetExtractos();
        ValueTask<LlaveValorDTO> CrearExtracto(InputExtracto input);
        ValueTask<List<MunicipiosDTO>> GetMunicipios();
        ValueTask<LlaveValorDTO> ActualizarExtracto(InputActualizarExtracto input);
    }
}
