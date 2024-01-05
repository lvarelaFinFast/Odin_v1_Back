using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestBodega.Data;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio.IRepositorio;
using static TestBodega.Util.Enum;

namespace TestBodega.Repositorio
{
    public class VehiculosRepositorio : IVechiculosRepositorio
    {
        private readonly TestBodegaContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public VehiculosRepositorio(TestBodegaContext testBodegaContext, IMapper mapper, IConfiguration configuration)
        {
            _context = testBodegaContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async ValueTask<List<PropietarioDTO>> GetPropietarios()
        {
            var propietarios = await _context.Propietarios.ToListAsync();
            var response = new List<PropietarioDTO>();

            foreach (var item in propietarios)
            {
                response.Add(_mapper.Map<PropietarioDTO>(item));
            }

            return response;
        }

        public async ValueTask<List<PropietarioDTO>> GetPropietario(string valorBusqueda)
        {
            var propietrios = await _context.Propietarios.Where(x => x.Documento.Contains(valorBusqueda) || x.Nombre.Contains(valorBusqueda)).ToListAsync();
            var response = new List<PropietarioDTO>();

            foreach (var item in propietrios)
            {
                response.Add(_mapper.Map<PropietarioDTO>(item));
            }

            return response;
        }

        public async Task<List<VehiculoDTO>> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculos.ToListAsync();

            var vehiculosDTO = new List<VehiculoDTO>();

            foreach (var item in vehiculos)
            {
                vehiculosDTO.Add(_mapper.Map<VehiculoDTO>(item));
            }

            return vehiculosDTO;
        }

        public async ValueTask<LlaveValorObjectDTO<Propietario>> GuardarPropietario(InputPropietario input)
        {
            var oPropietario = await _context.Propietarios.Where(x => x.Documento == input.Documento).FirstOrDefaultAsync();
            if (oPropietario == null)
            {
                var newPropietario = _mapper.Map<Propietario>(input);

                Propietario save = new Propietario();

                save = newPropietario;

                _context.Add(newPropietario);
                await _context.SaveChangesAsync();

                return new LlaveValorObjectDTO<Propietario>
                {
                    Llave = 0,
                    Valor = "Propietario Creado correctamente",
                    data = newPropietario
                };
            }
            else
            {
                return new LlaveValorObjectDTO<Propietario>
                {
                    Llave = 1,
                    Valor = "El propietario ya existe",
                    data = oPropietario
                };
            }
        }

        public async Task<LlaveValorDTO> GuardarVehiculo(InputVehiculo input)
        {
            var oVehiculo = await _context.Vehiculos.Where(x => x.Placa == input.Placa).FirstOrDefaultAsync();
            if (oVehiculo == null)
            {
                var newVehiculo = _mapper.Map<Vehiculo>(input);
                newVehiculo.IdEstado = (int)EnumEstadoConductor.Incompleto;
                newVehiculo.FechaCreacion = DateTime.Now;

                Vehiculo save = new Vehiculo();

                save = newVehiculo;

                _context.Add(save);
                await _context.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Vehiculo registrado correctamente"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "La placa ya existe!"
                };
            }
        }

        public async ValueTask<LlaveValorDTO> AsignarPropietario(InputAsignarPropietario input)
        {
            var relacion = await _context.VehiculosPropietarios.Where(x => x.IdPropietario == input.IdPropietario && x.IdVehiculo == input.IdVehiculo).FirstOrDefaultAsync();
            if (relacion == null)
            {
                var newRelacion = _mapper.Map<VehiculosPropietarios>(input);

                _context.Add(newRelacion);
                await _context.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Relacion registrada correctamente"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ya existe esta relacion!"
                };
            }
        }

        public async ValueTask<LlaveValorObjectDTO<EmpresaVinculante>> GuardarEmpresa(InputEmpresa input)
        {
            var oEmpresa = await _context.EmpresasVinculantes.Where(x => x.NIT == input.NIT).FirstOrDefaultAsync();
            if (oEmpresa == null)
            {
                var newEmpresa = _mapper.Map<EmpresaVinculante>(input);
                newEmpresa.FechaCreacion = DateTime.Now;

                EmpresaVinculante save = new EmpresaVinculante();

                save = newEmpresa;

                _context.Add(save);
                await _context.SaveChangesAsync();

                return new LlaveValorObjectDTO<EmpresaVinculante>
                {
                    Llave = 0,
                    Valor = "Empresa Creada correctamente",
                    data = newEmpresa
                };
            }
            else
            {
                return new LlaveValorObjectDTO<EmpresaVinculante>
                {
                    Llave = 1,
                    Valor = "La empresa ya existe",
                    data = oEmpresa
                };
            }
        }
        public async ValueTask<EmpresaVinculanteDTO> GetEmpresa(int idEmpresa)
        {
            var oEmpresa = await _context.EmpresasVinculantes.Where(x => x.Id == idEmpresa).FirstOrDefaultAsync();

            var response = _mapper.Map<EmpresaVinculanteDTO>(oEmpresa);

            return response;
        }

        public async ValueTask<EmpresaVinculanteDTO> GetEmpresaByNit(string nit)
        {
            var oEmpresa = await _context.EmpresasVinculantes.Where(x => x.NIT == nit).FirstOrDefaultAsync();

            var response = _mapper.Map<EmpresaVinculanteDTO>(oEmpresa);

            return response;
        }

        public async ValueTask<PropietarioDTO> GetPropietarioById(int id)
        {
            var oEmpresa = await _context.Propietarios.Where(x => x.Id == id).FirstOrDefaultAsync();

            var response = _mapper.Map<PropietarioDTO>(oEmpresa);

            return response;
        }

        public async ValueTask<LlaveValorDTO> ActualizarVehiculo(InputVehiculoUpdate input)
        {
            var oVehiculo = await _context.Vehiculos.Where(x => x.Placa == input.Placa).FirstOrDefaultAsync();
            if (oVehiculo != null)
            {

                oVehiculo.Placa = input.Placa;
                oVehiculo.NumeroInterno = input.NumeroInterno;
                oVehiculo.TipoVehiculo = input.TipoVehiculo;
                oVehiculo.CantidadPasajeros = input.CantidadPasajeros;
                oVehiculo.Marca = input.Marca;
                oVehiculo.Linea = input.Linea;
                oVehiculo.Modelo = input.Modelo;
                oVehiculo.FechaMatricula = input.FechaMatricula;
                oVehiculo.Color = input.Color;
                oVehiculo.Servicio = input.Servicio;
                oVehiculo.TipoCombustible = input.TipoCombustible;
                oVehiculo.NumeroMotor = input.NumeroMotor;
                oVehiculo.NumeroSerie = input.NumeroSerie;
                oVehiculo.IdPropietario = input.IdPropietario;
                oVehiculo.Convenio = input.Convenio;

                string mensajeResponse = "Vehiculo actualizado correctamente";

                if (input.Convenio)
                {
                    var oEmpresa = await _context.EmpresasVinculantes.Where(x => x.NIT == input.NitEmpresa).FirstOrDefaultAsync();

                    if (oEmpresa == null)
                    {
                        var newEmpresa = new InputEmpresa
                        {
                            NIT = input.NitEmpresa,
                            Nombre = input.NombreEmpresa
                        };

                        var crearEmpresa = await this.GuardarEmpresa(newEmpresa);

                        if (crearEmpresa.Llave == 1)
                        {
                            oVehiculo.IdEmpresaVinculante = crearEmpresa.data.Id;
                        }
                        else
                        {
                            mensajeResponse = "Vehiculo actualizado pero con error al crear la empresa!";
                        }
                    }
                }

                _context.Update(oVehiculo);
                await _context.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = mensajeResponse
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "El vehiculo no se encuentra registrado en la base de datos."
                };
            }
        }

        public async ValueTask<List<TipoArchivoDTO>> GetTipoArchivo()
        {
            return await _context.TipoArchivos.Where(x => x.Modulo == (int)EnumModulo.Vehiculos).Select(x => new TipoArchivoDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                FechaCreacion = x.FechaCreacion,
                Modulo = x.Modulo
            }).ToListAsync();
        }

        public async Task<List<ArchivosVehiculosDTO>> GetArchivos(int idVehiculo)
        {
            //Consulta archivos por idconductor y activos
            var data = await (from Archivos in _context.ArchivosVehiculos
                              join TipoArchivos in _context.TipoArchivos on Archivos.IdTipoArchivo equals TipoArchivos.Id
                              where Archivos.IdVehiculo == idVehiculo && Archivos.Activo && TipoArchivos.Modulo.Equals((int)EnumModulo.Vehiculos)
                              select new
                              {
                                  Archivos,
                                  TipoArchivos
                              }).ToListAsync();

            // preparamos respuesta
            List<ArchivosVehiculosDTO> response = new List<ArchivosVehiculosDTO>();

            foreach (var item in data)
            {
                var oResponse = new ArchivosVehiculosDTO
                {
                    Id = item.Archivos.Id,
                    IdVehiculo = item.Archivos.IdVehiculo,
                    IdTipoArchivo = item.Archivos.IdTipoArchivo,
                    NombreArchivo = item.Archivos.Ruta,
                    NmTipoArchivo = item.TipoArchivos.Nombre,
                    FechaVencimiento = item.Archivos.FechaVencimiento
                };

                response.Add(oResponse);
            }
            return response;

        }

        public async Task<LlaveValorDTO> ImportarArchivo(InputArchivosVehiculos input)
        {
            byte[] bytes = Convert.FromBase64String(input.Archivo);

            string pathFile = _configuration["PathFile"].ToString();
            string fileName = "";

            var tipoArchivo = await _context.TipoArchivos.Where(x => x.Id == input.IdTipoArchivo).FirstOrDefaultAsync();
            var vehiculo = await _context.Vehiculos.Where(x => x.Id == input.IdVehiculo).FirstOrDefaultAsync();


            fileName = tipoArchivo.Nombre + "-" + vehiculo.Placa + ".pdf";

            string newFilePath = Path.Combine(pathFile, fileName);

            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
                using (FileStream stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Close();
                }

                var oDocumento = await _context.ArchivosVehiculos.Where(x => x.IdVehiculo == input.IdVehiculo && x.IdTipoArchivo == input.IdTipoArchivo).FirstOrDefaultAsync();


                oDocumento.Ruta = newFilePath;
                oDocumento.FechaModificacion = DateTime.Now;


                _context.Update(oDocumento);


                await _context.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Archivo actualizado correctamente"
                };

            }
            else
            {
                using (FileStream stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Close();

                    var oDocumento = new ArchivosVehiculos
                    {
                        IdVehiculo = input.IdVehiculo,
                        IdTipoArchivo = input.IdTipoArchivo,
                        Ruta = newFilePath,
                        FechaCreacion = DateTime.Now.Date,
                        FechaVencimiento = input.FechaVencimiento,
                        Activo = true,
                        FechaModificacion = DateTime.Now.Date,
                    };

                    var listDocumentos = await _context.ArchivosVehiculos.Where(x => x.IdVehiculo == input.IdVehiculo).ToListAsync();
                    var listTipoArchivos = await _context.TipoArchivos.Where(x => x.Modulo == (int)EnumModulo.Vehiculos).ToListAsync();
                    int countDocs = 0;

                    foreach (var archivo in listDocumentos)
                    {
                        bool exist = listTipoArchivos.Where(x => x.Id == archivo.IdTipoArchivo).Any();
                        if (exist)
                        {
                            countDocs++;
                        }
                    }

                    if (countDocs == listTipoArchivos.Count())
                    {
                        vehiculo.IdEstado = (int)EnumEstadoConductor.Activo;
                        _context.Vehiculos.Update(vehiculo);
                    }

                    _context.ArchivosVehiculos.Add(oDocumento);
                    await _context.SaveChangesAsync();
                }

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Archivo guardado correctamente"
                };
            }
        }

        public async ValueTask<FileDTO> ObtenerArchivoVehiculos(string ruta)
        {
            byte[] archivoBytes = await File.ReadAllBytesAsync(ruta);

            string base64String = Convert.ToBase64String(archivoBytes);

            return new FileDTO
            {
                File = base64String
            };
        }
    }
}
