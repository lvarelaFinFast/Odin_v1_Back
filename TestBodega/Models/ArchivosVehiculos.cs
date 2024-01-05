using System;

namespace TestBodega.Models
{
    public class ArchivosVehiculos
    {
        public int Id { get; set; }
        public int IdVehiculo { get; set; }
        public int IdTipoArchivo { get; set; }
        public string Ruta { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
