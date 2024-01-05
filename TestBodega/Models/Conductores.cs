using System;
using System.ComponentModel.DataAnnotations;

namespace TestBodega.Models
{
    public class Conductores
    {
        [Key]
        public int Id { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string TipoLicencia { get; set; }
        public int NumeroPazYSalvo { get; set; }
        public int IdEstado { get; set; }
    }
}
