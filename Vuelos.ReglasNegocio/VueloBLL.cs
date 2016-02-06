using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.AccesoDatos;
using Vuelos.Entidades;

namespace Vuelos.ReglasNegocio
{
    public class VueloBLL : IVueloBLL
    {
        //private VueloDAL dal = new VueloDAL();
        private IVueloDAL dal;

        public VueloBLL(IVueloDAL pdal)
        {
            this.dal = pdal;
        }

        public IEnumerable<Vuelo> Listar(Int32? FiltroAerolinea)
        {
            return dal.Listar(FiltroAerolinea);
        }

        public Vuelo Obtener(Int32? Id)
        {
            return dal.Obtener(Id);
        }

        public void Crear(Vuelo Model)
        {
            if (Model.CiudadOrigen.Equals(Model.CiudadDestino))
            {
                throw new Exception("La ciudad origen y ciudad destino deben ser distintas.");
            }

            dal.Crear(Model);
        }

        public void Editar(Vuelo Model)
        {
            if (Model.CiudadOrigen.Equals(Model.CiudadDestino))
            {
                throw new Exception("La ciudad origen y ciudad destino deben ser distintas.");
            }

            dal.Editar(Model);
        }

        public void Eliminar(Vuelo Model)
        {
            dal.Eliminar(Model);
        }

        public void Dispose_()
        {
            dal.Dispose_();
        }
    }

    public interface IVueloBLL
    {
        IEnumerable<Vuelo> Listar(Int32? FiltroAerolinea);
        Vuelo Obtener(Int32? Id);
        void Crear(Vuelo Model);
        void Editar(Vuelo Model);
        void Eliminar(Vuelo Model);
        void Dispose_();
    }
}
