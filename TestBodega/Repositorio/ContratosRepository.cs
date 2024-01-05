using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TestBodega.Data;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using static TestBodega.Util.Enum;

namespace TestBodega.Repositorio
{
    public class ContratosRepository : IContratoRepository
    {
        private readonly IMapper _mapper;
        private readonly TestBodegaContext _context;
        public ContratosRepository(IMapper mapper, TestBodegaContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async ValueTask<LlaveValorDTO> ActualizarContrato(InputActualizarContrato input)
        {
            var oContrato = await _context.Contratos.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (oContrato == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error al actualizar la informacion del contrato"
                };
            }

            oContrato.NoContrato = input.NoContrato;
            oContrato.IdCliente = input.IdCliente;

            _context.Update(oContrato);
            await _context.SaveChangesAsync();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Contrato Actualizado correctamente"
            };
        }

        public async ValueTask<LlaveValorDTO> EliminarContrato(int idContrato)
        {
            var oContrato = await _context.Contratos.Where(x => x.Id == idContrato).FirstOrDefaultAsync();
            if (oContrato == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error al eliminar la informacion del contrato"
                };
            }

            _context.Remove(oContrato);
            await _context.SaveChangesAsync();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Contrato Eliminado correctamente"
            };
        }

        public async ValueTask<List<ContratoDTO>> GetContratos()
        {
            var data = await (from contrato in _context.Contratos
                              join cliente in _context.Clientes on contrato.IdCliente equals cliente.Id
                              select new
                              {
                                  contrato,
                                  cliente
                              }).ToListAsync();
            List<ContratoDTO> listContratoDTO = new List<ContratoDTO>();

            foreach (var item in data)
            {
                var contratoDTO = new ContratoDTO
                {
                    Id = item.contrato.Id,
                    NoContrato = item.contrato.NoContrato,
                    IdCliente = item.contrato.IdCliente,
                    NmCliente = item.cliente.NmCliente
                };

                listContratoDTO.Add(contratoDTO);
            }

            return listContratoDTO;
        }

        public async ValueTask<LlaveValorDTO> GuardarContrato(InputContrato input)
        {
            var oContrato = await _context.Contratos.Where(x => x.NoContrato == input.NoContrato).FirstOrDefaultAsync();
            if (oContrato != null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ya existe un contrato con este numero"
                };
            }

            var newCliente = _mapper.Map<Contrato>(input);

            Contrato saveCliente = new Contrato();

            saveCliente = newCliente;

            _context.Add(saveCliente);
            await _context.SaveChangesAsync();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Contrato Creado correctamente"
            };
        }

        public async ValueTask<LlaveValorDTO> CrearExtracto(InputExtracto input)
        {
            LlaveValorDTO validacion = await this.validacionesExtracto(input);
            if (validacion.Llave != 0)
            {
                return new LlaveValorDTO
                {
                    Llave = validacion.Llave,
                    Valor = validacion.Valor
                };
            }

            var extracto = new Extracto
            {
                IdOrigen = input.IdOrigen,
                IdDestino = input.IdDestino,
                IdaYvuelta = input.IdaYvuelta,
                IdContrato = input.IdContrato,
                IdVehiculo = input.IdVehiculo,
                IdConductor1 = input.IdConductor1,
                idConductor2 = input.idConductor2,
                FechaInicio = input.FechaInicio,
                FechaFinal = input.FechaFinal,
                FechaCreacion = DateTime.Now,
                Direccion = input.Direccion,
                Correo = input.Correo,
                Telefono1 = input.Telefono1,
                Telefono2 = input.Telefono2
            };

            _context.Extractos.Add(extracto);
            _context.SaveChanges();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Extracto Creado correctamente"
            };

        }

        public async ValueTask<List<ExtractoDTO>> GetExtractos()
        {
            List<ExtractoDTO> extractos = new List<ExtractoDTO>();
            var query = await (from e in _context.Extractos
                               join origen in _context.Municipios on e.IdOrigen equals origen.Id
                               join destino in _context.Municipios on e.IdDestino equals destino.Id
                               join c in _context.Contratos on e.IdContrato equals c.Id
                               join v in _context.Vehiculos on e.IdVehiculo equals v.Id
                               join ct1 in _context.Conductores on e.IdConductor1 equals ct1.Id
                               join ct2 in _context.Conductores on e.idConductor2 equals ct2.Id into conductores2Group
                               from ct2 in conductores2Group.DefaultIfEmpty()
                               select new queryExtractoDTO
                               {
                                   Extracto = e,
                                   Origen = origen,
                                   Destino = destino,
                                   Contrato = c,
                                   Vehiculo = v,
                                   Conductores1 = ct1,
                                   conductores2 = ct2
                               }).ToListAsync();
            if (query == null)
            {
                return extractos;
            }
            foreach (var item in query)
            {
                var extracto = new ExtractoDTO
                {
                    Id = item.Extracto.Id,
                    IdOrigen = item.Extracto.IdOrigen,
                    NmOrigen = item.Origen.Nombre,
                    IdDestino = item.Extracto.IdDestino,
                    NmDestino = item.Destino.Nombre,
                    IdaYvuelta = item.Extracto.IdaYvuelta,
                    IdContrato = item.Extracto.Id,
                    NoContrato = item.Contrato.NoContrato,
                    IdVehiculo = item.Extracto.IdVehiculo,
                    placa = item.Vehiculo.Placa,
                    IdConductor1 = item.Extracto.IdConductor1,
                    NmConductor1 = item.Conductores1.Nombre,
                    idConductor2 = item.Extracto.idConductor2 != null ? item.Extracto.idConductor2 : null,
                    NmConductor2 = item.conductores2 != null ? item.conductores2.Nombre : null,
                    FechaInicio = item.Extracto.FechaInicio,
                    FechaFinal = item.Extracto.FechaFinal,
                    FechaCreacion = item.Extracto.FechaCreacion,
                    Direccion = item.Extracto.Direccion,
                    Correo = item.Extracto.Correo,
                    Telefono1 = item.Extracto.Telefono1,
                    Telefono2 = item.Extracto.Telefono2
                };

                extractos.Add(extracto);
            }

            return extractos;
        }

        public async ValueTask<List<MunicipiosDTO>> GetMunicipios()
        {
            return await _context.Municipios.Select(x => new MunicipiosDTO { Id = x.Id, Nombre = x.Nombre }).ToListAsync();
        }

        private async Task<LlaveValorDTO> validacionesExtracto(InputExtracto input)
        {
            //validar disponibilidad del vehiculo
            var vehiculoDisponible = await _context.Vehiculos.Where(x => x.Id == input.IdVehiculo && x.IdEstado == 1).FirstOrDefaultAsync();
            if (vehiculoDisponible == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un consultando la informacion del vehiculo seleccionado o no se encuentra activo, revise el estado del vehiculo"
                };
            }

            //validar disponibilidad de documentos del vehiculo
            var documentosCountVehiculos = await _context.TipoArchivos.Where(x => x.Modulo == (int)EnumModulo.Vehiculos).CountAsync();
            var documentosVehiculo = await _context.ArchivosVehiculos.Where(x => x.IdVehiculo == input.IdVehiculo).ToListAsync();

            if (documentosVehiculo.Count < documentosCountVehiculos)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "El vehiculo no cuenta con los documentos necesarios para crear el extracto"
                };
            }

            foreach (var item in documentosVehiculo)
            {
                var nmTipoArchivo = await _context.TipoArchivos.Where(x => x.Id == item.IdTipoArchivo).Select(x => x.Nombre).FirstOrDefaultAsync();
                if (item.FechaVencimiento <= DateTime.Now)
                {
                    return new LlaveValorDTO
                    {
                        Llave = 1,
                        Valor = $"El Vehiculo cuenta el documento: {nmTipoArchivo} vencido."
                    };
                }
            }

            //Validar Que no tenga viajes en las fechas seleccionada
            if (input.IdaYvuelta)
            {
                bool tieneExtracto = _context.Extractos.Any(x => x.IdVehiculo == input.IdVehiculo && (x.FechaInicio <= input.FechaFinal && x.FechaFinal >= input.FechaInicio));
                if (tieneExtracto)
                {
                    return new LlaveValorDTO
                    {
                        Llave = 1,
                        Valor = "El vehiculo cuenta con un extracto creado para las fechas seleccionadas"
                    };
                }
            }

            //Validar estado de Conductores
            var conductor = await _context.Conductores.Where(x => x.Id == input.IdConductor1 && x.IdEstado == (int)EnumEstadoConductor.Activo).FirstOrDefaultAsync();
            if (conductor == null)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrió un consultando la informacion del conductor seleccionado o no se encuentra activo, revise el estado del conductor"
                };
            }

            //validar disponibilidad de documentos del conductor
            var documentosCountConductor = await _context.TipoArchivos.Where(x => x.Modulo == (int)EnumModulo.Conductores).CountAsync();
            var documentosconductor = await _context.Archivos.Where(x => x.IdConductor == input.IdConductor1).ToListAsync();

            if (documentosconductor.Count < documentosCountConductor)
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "El conductor no cuenta con los documentos necesarios para crear el extracto"
                };
            }

            foreach (var item in documentosconductor)
            {
                var nmTipoArchivo = await _context.TipoArchivos.Where(x => x.Id == item.IdTipoArchivo).Select(x => x.Nombre).FirstOrDefaultAsync();
                if (item.FechaVencimiento <= DateTime.Now)
                {
                    return new LlaveValorDTO
                    {
                        Llave = 1,
                        Valor = $"El Conductor cuenta el documento: {nmTipoArchivo} vencido."
                    };
                }
            }

            //Validar Que no tenga viajes en las fechas seleccionada
            if (input.IdaYvuelta)
            {
                bool tieneExtracto = _context.Extractos.Any(x => x.IdConductor1 == input.IdConductor1 && (x.FechaInicio <= input.FechaFinal && x.FechaFinal >= input.FechaInicio));
                if (tieneExtracto)
                {
                    return new LlaveValorDTO
                    {
                        Llave = 1,
                        Valor = "El vehiculo cuenta con un extracto creado para las fechas seleccionadas"
                    };
                }
            }

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Validaciones correctas"
            };
        }

        public async ValueTask<LlaveValorDTO> ActualizarExtracto(InputActualizarExtracto input)
        {
            var data = await _context.Extractos.Where(x => x.Id == input.Id).FirstOrDefaultAsync();

            data.IdOrigen = input.IdOrigen;
            data.IdDestino = input.IdDestino;
            data.IdaYvuelta = input.IdaYvuelta;
            data.IdContrato = input.IdContrato;
            data.IdVehiculo = input.IdVehiculo;
            data.IdConductor1 = input.IdConductor1;
            data.idConductor2 = input.idConductor2;
            data.FechaInicio = input.FechaInicio;
            data.FechaFinal = input.FechaFinal;
            data.Direccion = input.Direccion;
            data.Correo = input.Correo;
            data.Telefono1 = input.Telefono1;
            data.Telefono2 = input.Telefono2;

            _context.Extractos.Update(data);
            _context.SaveChanges();

            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = "Extracto Actualizado correctamente"
            };
        }
    }
}

