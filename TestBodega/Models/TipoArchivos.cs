using System;

namespace TestBodega.Models
{
    public class TipoArchivos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int Modulo { get; set; }
    }
}
