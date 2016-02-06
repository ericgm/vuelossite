using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.AccesoDatos;
using Vuelos.Entidades;

namespace Vuelos.ReglasNegocio
{
    public class AerolineaBLL : IAerolineaBLL
    {
        //private AerolineaDAL dal = new AerolineaDAL();
        private IAerolineaDAL dal;

        public AerolineaBLL(IAerolineaDAL pdal)
        {
            this.dal = pdal;
        }

        public IEnumerable<Aerolinea> Listar()
        {
            return dal.Listar();
        }

        public Aerolinea Obtener(Int32? Id)
        {
            return dal.Obtener(Id);
        }

        public Aerolinea ObtenerCompleto(Int32? Id)
        {
            return dal.ObtenerCompleto(Id);
        }

        public void Crear(Aerolinea Model)
        {
            dal.Crear(Model);
        }

        public void Editar(Aerolinea Model)
        {
            dal.Editar(Model);
        }

        public void Eliminar(Aerolinea Model)
        {
            dal.Eliminar(Model);
        }

        public void Dispose_()
        {
            dal.Dispose_();
        }
    }

    public interface IAerolineaBLL
    {
        IEnumerable<Aerolinea> Listar();
        Aerolinea Obtener(Int32? Id);
        Aerolinea ObtenerCompleto(Int32? Id);
        void Crear(Aerolinea Model);
        void Editar(Aerolinea Model);
        void Eliminar(Aerolinea Model);
        void Dispose_();
    }
}
