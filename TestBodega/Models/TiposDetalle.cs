﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TestBodega.Models
{
    public partial class TiposDetalle
    {
        public TiposDetalle()
        {
            MovimientoInventarios = new HashSet<MovimientoInventario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; }
    }
}
