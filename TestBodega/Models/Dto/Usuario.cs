using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestBodega.Models.Dto
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {

        }

        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Rol { get; set; }
    }
}
