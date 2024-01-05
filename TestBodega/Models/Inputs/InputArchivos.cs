using System;

namespace TestBodega.Models.Inputs
{
    public class InputArchivos
    {
        public int IdConductor { get; set; }
        public int IdTipoArchivo { get; set; }
        public string Archivo { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
