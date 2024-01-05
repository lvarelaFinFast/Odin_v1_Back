using System.Collections.Generic;
using System.Threading.Tasks;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;

namespace TestBodega.Repositorio.IRepositorio
{
    public interface IClienteRepository
    {
        ValueTask<List<ClienteDTO>> GetClientes();
        ValueTask<LlaveValorDTO> GuardarCliente(InputCliente input);
        ValueTask<LlaveValorDTO> ActualizarCliente(InputActualizarCliente input);
        ValueTask<LlaveValorDTO> EliminarCliente(int idCliente);
    }
}
