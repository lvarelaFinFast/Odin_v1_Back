using System;

namespace TestBodega.Models.Inputs
{
    public class InputExtracto
    {
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public bool IdaYvuelta { get; set; }
        public int IdContrato { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConductor1 { get; set; }
        public int? idConductor2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
    }
}
