using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBodega.Data;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio.IRepositorio;

namespace TestBodega.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly TestBodegaContext _dbContext;
        private readonly IMapper _mapper;

        public ClienteRepository(IConfiguration configuration, TestBodegaContext context, IMapper mapper)
        {
            _configuration = configuration;
            _dbContext = context;
            _mapper = mapper;
        }

        public async ValueTask<LlaveValorDTO> ActualizarCliente(InputActualizarCliente input)
        {
            var oCliente = await _dbContext.Clientes.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (oCliente == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error al actualizar la informacion del cliente"
                };
            }

            oCliente.IdTipoPersona = input.IdTipoPersona;
            oCliente.NmCliente = input.NmCliente;
            oCliente.IdTipoDocumento = input.IdTipoDocumento;
            oCliente.NoDocumento = input.NoDocumento;
            oCliente.Telefono = input.Telefono;
            oCliente.Correo = input.Correo;

            _dbContext.Update(oCliente);
            await _dbContext.SaveChangesAsync();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Cliente Actualizado correctamente"
            };
        }

        public async ValueTask<LlaveValorDTO> EliminarCliente(int idCliente)
        {
            var oCliente = await _dbContext.Clientes.Where(x => x.Id == idCliente).FirstOrDefaultAsync();
            if (oCliente == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error al eliminar la informacion del cliente"
                };
            }

            _dbContext.Remove(oCliente);
            await _dbContext.SaveChangesAsync();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Cliente Eliminado correctamente"
            };
        }

        public async ValueTask<List<ClienteDTO>> GetClientes()
        {
            var data = await _dbContext.Clientes.ToListAsync();
            List<ClienteDTO> listClienteDTO = new List<ClienteDTO>();

            foreach (var item in data)
            {
                listClienteDTO.Add(_mapper.Map<ClienteDTO>(item));
            }

            return listClienteDTO;
        }

        public async ValueTask<LlaveValorDTO> GuardarCliente(InputCliente input)
        {
            var oCliente = await _dbContext.Clientes.Where(x => x.NoDocumento == input.NoDocumento).FirstOrDefaultAsync();
            if (oCliente == null)
            {
                var newCliente = _mapper.Map<Cliente>(input);

                Cliente saveCliente = new Cliente();

                saveCliente = newCliente;

                _dbContext.Add(saveCliente);
                await _dbContext.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Cliente Creada correctamente"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Cliente Ya existe!"
                };
            }
        }
    }
}
