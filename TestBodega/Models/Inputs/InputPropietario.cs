using System;

namespace TestBodega.Models.Inputs
{
    public class InputPropietario
    {
        public string Nombre { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
