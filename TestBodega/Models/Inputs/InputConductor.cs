using System;

namespace TestBodega.Models.Inputs
{
    public class InputConductor
    {
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string TipoLicencia { get; set; }
        public int NumeroPazYSalvo { get; set; }
    }
}
