using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.AccesoDatos;
using Vuelos.Entidades;

namespace Vuelos.ReglasNegocio
{
    public class CiudadBLL : ICiudadBLL
    {
        //private CiudadDAL dal = new CiudadDAL();
        private ICiudadDAL dal;

        public CiudadBLL(ICiudadDAL pdal)
        {
            this.dal = pdal;
        }

        public IEnumerable<Ciudad> Listar()
        {
            return dal.Listar();
        }

        public Ciudad Obtener(String Id)
        {
            return dal.Obtener(Id);
        }

        public IList<Ciudad> Buscar(String filtro)
        {
            return dal.Buscar(filtro);
        }

        public void Crear(Ciudad Model)
        {
            dal.Crear(Model);
        }

        public void Editar(Ciudad Model)
        {
            dal.Editar(Model);
        }

        public void Eliminar(Ciudad Model)
        {
            dal.Eliminar(Model);
        }

        public void Dispose_()
        {
            dal.Dispose_();
        }
    }

    public interface ICiudadBLL
    {
        IEnumerable<Ciudad> Listar();
        Ciudad Obtener(String Id);
        IList<Ciudad> Buscar(String filtro);
        void Crear(Ciudad Model);
        void Editar(Ciudad Model);
        void Eliminar(Ciudad Model);
        void Dispose_();
    }
}
