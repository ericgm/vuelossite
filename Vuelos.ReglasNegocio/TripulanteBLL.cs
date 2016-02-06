using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;
using Vuelos.AccesoDatos;

namespace Vuelos.ReglasNegocio
{
    public class TripulanteBLL : ITripulanteBLL
    {
        //private TripulanteDAL dal = new TripulanteDAL();
        private ITripulanteDAL dal;

        public TripulanteBLL(ITripulanteDAL pdal)
        {
            this.dal = pdal;
        }

        public IEnumerable<Tripulante> ListarTodos()
        {
            return dal.ListarTodos();
        }

        public IEnumerable<Tripulante> Listar()
        {
            return dal.Listar();
        }

        public Tripulante Obtener(Int32? Id)
        {
            return dal.Obtener(Id);
        }

        public void Crear(Tripulante Model)
        {
            dal.Crear(Model);
        }

        public void Editar(Tripulante Model)
        {
            dal.Editar(Model);
        }

        public void Eliminar(Tripulante Model)
        {
            dal.Eliminar(Model);
        }

        public void Dispose_()
        {
            dal.Dispose_();
        }
    }

    public interface ITripulanteBLL
    {
        IEnumerable<Tripulante> ListarTodos();
        IEnumerable<Tripulante> Listar();
        Tripulante Obtener(Int32? Id);
        void Crear(Tripulante Model);
        void Editar(Tripulante Model);
        void Eliminar(Tripulante Model);
        void Dispose_();
    }
}
