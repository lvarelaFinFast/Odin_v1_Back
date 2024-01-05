using System;

namespace TestBodega.Models.Inputs
{
    public class InputVehiculo
    {
        public string Placa { get; set; }
        public string NumeroInterno { get; set; }
        public string TipoVehiculo { get; set; }
        public int CantidadPasajeros { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int Modelo { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Color { get; set; }
        public string Servicio { get; set; }
        public string TipoCombustible { get; set; }
        public string NumeroMotor { get; set; }
        public string NumeroSerie { get; set; }
        public int IdPropietario { get; set; }
        public bool Convenio { get; set; }
        public int IdEmpresaVinculante { get; set; }
    }
}
