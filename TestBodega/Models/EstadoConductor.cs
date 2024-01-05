using System;
using System.Data.Common;

namespace TestBodega.Models
{
    public class EstadoConductor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
