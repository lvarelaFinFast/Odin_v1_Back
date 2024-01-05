using System;

namespace TestBodega.Models.Dto
{
    public class ArchivosVehiculosDTO
    {
        public int Id { get; set; }
        public int IdVehiculo { get; set; }
        public int IdTipoArchivo { get; set; }
        public string NmTipoArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
