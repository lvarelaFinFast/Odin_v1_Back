﻿using System;

namespace TestBodega.Models.Dto
{
    public class ExtractoDTO
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public string NmOrigen { get; set; }
        public int IdDestino { get; set; }
        public string NmDestino { get; set; }
        public bool IdaYvuelta { get; set; }
        public int IdContrato { get; set; }
        public int NoContrato { get; set; }
        public int IdVehiculo { get; set; }
        public string placa { get; set; }
        public int IdConductor1 { get; set; }
        public string NmConductor1 { get; set; }
        public int? idConductor2 { get; set; }
        public string NmConductor2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }

    }
}
