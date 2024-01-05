using System;

namespace TestBodega.Models
{
    public class DataReporteDTO
    {
        public string RazonSocial { get; set; }
        public string NitEmpresa { get; set; }
        public int ExtractoId { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public bool IdaYvuelta { get; set; }
        public int IdContrato { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConductor1 { get; set; }
        public int IdConductor2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime ExtractoFechaCreacion { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public int VehiculoId { get; set; }
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
        public DateTime VehiculoFechaCreacion { get; set; }
        public int VehiculoEstadoId { get; set; }
        public int Conductor1Id { get; set; }
        public int Conductor1TipoDocumento { get; set; }
        public string Conductor1Documento { get; set; }
        public string NombreConductor1 { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Conductor2Id { get; set; }
        public int Conductor2TipoDocumento { get; set; }
        public string Conductor2Documento { get; set; }
        public string Conductor2Nombre { get; set; }
        public int ContratoId { get; set; }
        public int NoContrato { get; set; }
        public int IdCliente { get; set; }
        public int ClienteId { get; set; }
        public int IdTipoPersona { get; set; }
        public string NmCliente { get; set; }
        public int ClienteTipoDocumento { get; set; }
        public string ClienteNoDocumento { get; set; }
        public string NombreCliente { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteCorreo { get; set; }
        public string NombreOrigen { get; set; }
        public string NombreDestino { get; set; }
    }
}
