using System;

namespace TestBodega.Models.Dto
{
    public class ArchivosDTO
    {
        public int Id { get; set; }
        public int IdConductor { get; set; }
        public int IdTipoArchivo { get; set; }
        public string NmTipoArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
