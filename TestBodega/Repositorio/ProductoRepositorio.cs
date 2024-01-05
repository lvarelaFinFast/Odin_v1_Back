using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using TestBodega.Data;
using TestBodega.Models;
using TestBodega.Repositorio.IRepositorio;
using System;
using TestBodega.Models.Dto;

namespace TestBodega.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly TestBodegaContext _bd;

        public ProductoRepositorio(TestBodegaContext bd)
        {
            _bd = bd;
        }


        public bool CrearProducto(Producto producto)
        {
            producto.CodigoProducto = this.GenerarCodigoProducto();
            producto.Estado = "E";
            producto.FechaRegistro = DateTime.Now;
            producto.FechaModificacion = DateTime.Now;
            _bd.Add(producto);
            return Guardar();
        }



        public bool ExisteCodigoProducto(int codigoProducto)
        {
            throw new System.NotImplementedException();
        }

        public bool ExisteProducto(int id)
        {
            throw new System.NotImplementedException();
        }

        public Producto GetProducto(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Producto> GetProductos()
        {
            return _bd.Productos.ToList();
        }

        public ICollection<Producto> GetProductosDefectuosos()
        {
            return _bd.Productos.Where(p => p.Estado == "D").ToList();

        }

        public ICollection<Producto> GetProductosOptimos()
        {
            return _bd.Productos.Where(p => p.Estado == "O").ToList();
        }

        public bool MarcarDefectuoso(Producto producto)
        {
            producto.Estado = "D";
            _bd.Update(producto);
            return Guardar();
        }
        public int GenerarCodigoProducto()
        {
            Random random = new Random();

            int NumeroDigitos = 6;

            int min = (int)Math.Pow(10, NumeroDigitos - 1);
            int max = (int)Math.Pow(10, NumeroDigitos) - 1;

            int NumeroGenerado = random.Next(min, max);

            return (NumeroGenerado);
        }
        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public bool MarcarOptimo(Producto producto)
        {
            producto.Estado = "O";
            producto.FechaModificacion = DateTime.Now;
            _bd.Update(producto);
            return Guardar();
        }

        #region User

        public ICollection<Usuario> GetUserList()
        {
            var oReturn = _bd.Usuarios.ToList();
            return oReturn;
        }

        public Usuario GetUserAuth(Usuario usuario)
        {
            var oReturn = _bd.Usuarios.Where(x => x.User == usuario.User && x.Password == usuario.Password).FirstOrDefault();

            if (oReturn == null)
            {
                throw new ArgumentException("Usuario o contraseña invalida");
            }
            return oReturn;
        }

        public ICollection<Usuario> UpsertUsuario(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                _bd.Add(usuario);
            }
            else
            {
                _bd.Update(usuario);
            }
            
            Guardar();
            return _bd.Usuarios.ToList();

        }

        #endregion
    }
}
