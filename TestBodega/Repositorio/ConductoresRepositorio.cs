using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TestBodega.Data;
using TestBodega.Models;
using TestBodega.Models.Dto;
using TestBodega.Models.Inputs;
using TestBodega.Repositorio.IRepositorio;
using static TestBodega.Util.Enum;
using ZXing;
using ZXing.QrCode;
using Microsoft.Data.SqlClient;

namespace TestBodega.Repositorio
{
    public class ConductoresRepositorio : IConductoresRepositorio
    {
        private readonly TestBodegaContext _bd;
        private readonly IConfiguration _configuration;

        public ConductoresRepositorio(TestBodegaContext bd, IConfiguration configuration)
        {
            _bd = bd;
            _configuration = configuration;
        }

        public async Task<PaginatedList<ConductoresDTO>> GetConductores(int pageNumber, int pageSize)
        {
            var conductores = _bd.Conductores.Where(x => x.IdEstado != (int)EnumEstadoConductor.Eliminado).Select(x => new ConductoresDTO
            {
                Id = x.Id,
                IdTipoDocumento = x.IdTipoDocumento,
                Documento = x.Documento,
                Nombre = x.Nombre,
                FechaNacimiento = x.FechaNacimiento,
                Celular = x.Celular,
                Correo = x.Correo,
                TipoLicencia = x.TipoLicencia,
                NumeroPazYSalvo = x.NumeroPazYSalvo,
                Estado = _bd.EstadoConductors.Where(c => c.Id == x.IdEstado).Select(c => c.Nombre).FirstOrDefault()
            });

            var paginatedConductores = await PaginatedList<ConductoresDTO>.CreateAsync(conductores, pageNumber, pageSize);

            return paginatedConductores;
        }

        public async Task<List<ArchivosDTO>> GetArchivos(int Idconductor)
        {
            //Consulta archivos por idconductor y activos
            var data = await (from Archivos in _bd.Archivos
                              join TipoArchivos in _bd.TipoArchivos on Archivos.IdTipoArchivo equals TipoArchivos.Id
                              where Archivos.IdConductor == Idconductor && Archivos.Activo && TipoArchivos.Modulo.Equals((int)EnumModulo.Conductores)
                              select new
                              {
                                  Archivos,
                                  TipoArchivos
                              }).ToListAsync();

            // preparamos respuesta
            List<ArchivosDTO> response = new List<ArchivosDTO>();

            foreach (var item in data)
            {
                var oResponse = new ArchivosDTO
                {
                    Id = item.Archivos.Id,
                    IdConductor = item.Archivos.IdConductor,
                    IdTipoArchivo = item.Archivos.IdTipoArchivo,
                    NombreArchivo = item.Archivos.Ruta,
                    NmTipoArchivo = item.TipoArchivos.Nombre,
                    FechaVencimiento = item.Archivos.FechaVencimiento
                };

                response.Add(oResponse);
            }
            return response;

        }

        public async Task<LlaveValorDTO> GuardarConductor(InputConductor input)
        {
            if (input != null)
            {
                var oConductor = new Conductores
                {
                    IdTipoDocumento = input.IdTipoDocumento,
                    Documento = input.Documento,
                    Nombre = input.Nombre,
                    FechaNacimiento = input.FechaNacimiento,
                    Correo = input.Correo,
                    Celular = input.Celular,
                    TipoLicencia = input.TipoLicencia,
                    NumeroPazYSalvo = input.NumeroPazYSalvo,
                    IdEstado = (int)EnumEstadoConductor.Incompleto
                };

                _bd.Add(oConductor);
                await _bd.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Conductor creado con exito"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error en la creacion del conductor"
                };
            }
        }

        public async Task<LlaveValorDTO> ActualizarConductor(InputConductor input)
        {
            var oConductor = await _bd.Conductores.Where(X => X.Documento == input.Documento).FirstOrDefaultAsync();
            if (oConductor != null)
            {
                oConductor.IdTipoDocumento = input.IdTipoDocumento;
                oConductor.Documento = input.Documento;
                oConductor.Nombre = input.Nombre;
                oConductor.Celular = input.Celular;
                oConductor.Correo = input.Correo;
                oConductor.TipoLicencia = input.TipoLicencia;
                oConductor.FechaNacimiento = input.FechaNacimiento;
                oConductor.NumeroPazYSalvo = input.NumeroPazYSalvo;

                _bd.Update(oConductor);
                await _bd.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Conductor actualizado con exito"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error en la actualizacion del conductor"
                };
            }
        }

        public async Task<LlaveValorDTO> DeleteConductor(int Idconductor)
        {
            var oConductor = await _bd.Conductores.Where(X => X.Id == Idconductor).FirstOrDefaultAsync();

            if (oConductor != null)
            {
                _bd.Remove(oConductor);
                await _bd.SaveChangesAsync();

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Conductor Eliminado con exito"
                };
            }
            else
            {
                return new LlaveValorDTO
                {
                    Llave = 1,
                    Valor = "Ocurrio un error en la eliminacion del conductor"
                };
            }
        }

        public async Task<List<TipoArchivoDTO>> GetTipoArchivos()
        {
            return await _bd.TipoArchivos.Select(x => new TipoArchivoDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                FechaCreacion = x.FechaCreacion,
                Modulo = x.Modulo
            }).ToListAsync();
        }

        public async Task<List<ConductoresDTO>> GetConductoresWhitOutPaginator()
        {
            var conductores = await _bd.Conductores.Where(x => x.IdEstado != (int)EnumEstadoConductor.Eliminado).Select(x => new ConductoresDTO
            {
                Id = x.Id,
                IdTipoDocumento = x.IdTipoDocumento,
                Documento = x.Documento,
                Nombre = x.Nombre,
                FechaNacimiento = x.FechaNacimiento,
                Celular = x.Celular,
                Correo = x.Correo,
                TipoLicencia = x.TipoLicencia,
                NumeroPazYSalvo = x.NumeroPazYSalvo,
                Estado = _bd.EstadoConductors.Where(c => c.Id == x.IdEstado).Select(c => c.Nombre).FirstOrDefault()
            }).ToListAsync();

            return conductores;
        }

        public async Task<LlaveValorDTO> ImportarArchivo(InputArchivos input)
        {
            byte[] bytes = Convert.FromBase64String(input.Archivo);

            string pathFile = _configuration["PathFile"].ToString();
            string fileName = "";

            var tipoArchivo = await _bd.TipoArchivos.Where(x => x.Id == input.IdTipoArchivo).FirstOrDefaultAsync();
            var conductor = await _bd.Conductores.Where(x => x.Id == input.IdConductor).FirstOrDefaultAsync();


            fileName = tipoArchivo.Nombre + "-" + conductor.Documento + ".pdf";

            string newFilePath = Path.Combine(pathFile, fileName);

            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
                using (FileStream stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Close();
                }

                var oDocumento = await _bd.Archivos.Where(x => x.IdConductor == input.IdConductor && x.IdTipoArchivo == input.IdTipoArchivo).FirstOrDefaultAsync();


                oDocumento.Ruta = newFilePath;
                oDocumento.FechaModificacion = DateTime.Now;


                _bd.Update(oDocumento);


                await _bd.SaveChangesAsync();

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

                    var oDocumento = new Archivos
                    {
                        IdConductor = input.IdConductor,
                        IdTipoArchivo = input.IdTipoArchivo,
                        Ruta = newFilePath,
                        FechaCreacion = DateTime.Now.Date,
                        FechaVencimiento = input.FechaVencimiento,
                        Activo = true,
                        FechaModificacion = DateTime.Now.Date,
                    };

                    var listDocumentos = await _bd.Archivos.Where(x => x.IdConductor == input.IdConductor).ToListAsync();
                    var listTipoArchivos = await _bd.TipoArchivos.Where(x => x.Modulo == (int)EnumModulo.Conductores).ToListAsync();
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
                        conductor.IdEstado = (int)EnumEstadoConductor.Activo;
                        _bd.Conductores.Update(conductor);
                    }

                    _bd.Archivos.Add(oDocumento);
                    await _bd.SaveChangesAsync();
                }

                return new LlaveValorDTO
                {
                    Llave = 0,
                    Valor = "Archivo guardado correctamente"
                };
            }
        }

        public async ValueTask<FileDTO> ObtenerArchivo(string ruta)
        {
            byte[] archivoBytes = await File.ReadAllBytesAsync(ruta);

            string base64String = Convert.ToBase64String(archivoBytes);

            return new FileDTO
            {
                File = base64String
            };
        }

        public async Task<LlaveValorDTO> GenerarArchivo(int idExtracto)
        {
            string sql = "dataReporte @idExtracto";

            List<SqlParameter> @parms = new()
                {
                new SqlParameter { ParameterName = "@idExtracto", Value = idExtracto }
                };

            // Consultamos datos.
            var Data = await _bd.SPDataReporte.FromSqlRaw<DataReporteDTO>(sql, @parms.ToArray()).ToListAsync();

            var report = new LocalReport();
            using var rs = new FileStream("C:\\Files\\FileFUEC\\Report1.rdl", FileMode.Open);
            report.LoadReportDefinition(rs);
            report.DataSources.Add(new ReportDataSource("DataSet1", Data));

            var file = report.Render("PDF");
            return new LlaveValorDTO
            {
                Llave = 0,
                Valor = Convert.ToBase64String(file)
            };
        }
        private string ConvertToBase64QrCode(string data)
        {
            var qrCodeBitmap = QrCodeGenerator.GenerateQrCodeForUrl(data);
            using var memoryStream = new MemoryStream();
            qrCodeBitmap.Save(memoryStream, ImageFormat.Png);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

    }

    public class QrCodeGenerator
    {
        public static Bitmap GenerateQrCodeForUrl(string url, int width = 300, int height = 300)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };

            var pixelData = barcodeWriter.Write(url);
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb);

            for (var y = 0; y < pixelData.Height; y++)
            {
                for (var x = 0; x < pixelData.Width; x++)
                {
                    var color = Color.FromArgb(
                        pixelData.Pixels[y * pixelData.Width + x]);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
