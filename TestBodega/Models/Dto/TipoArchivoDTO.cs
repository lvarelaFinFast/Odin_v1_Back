﻿using System;

namespace TestBodega.Models.Dto
{
    public class TipoArchivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int Modulo { get; set; }
    }
}
