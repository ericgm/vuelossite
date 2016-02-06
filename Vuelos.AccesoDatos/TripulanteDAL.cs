using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;

namespace Vuelos.AccesoDatos
{
    public class TripulanteDAL : ITripulanteDAL
    {
        private VueloDBContext db = new VueloDBContext();

        public IEnumerable<Tripulante> ListarTodos()
        {
            return db.Tripulantes.ToList();
        }

        public IEnumerable<Tripulante> Listar()
        {
            var tripulantes = db.Tripulantes.OrderBy(x => x.Nombre).Include(t => t.Vuelo);
            return tripulantes.ToList();
        }

        public Tripulante Obtener(Int32? Id)
        {
            return db.Tripulantes.Find(Id);
        }

        public void Crear(Tripulante Model)
        {
            db.Tripulantes.Add(Model);
            db.SaveChanges();
        }

        public void Editar(Tripulante Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(Tripulante Model)
        {
            db.Entry(Model).State = EntityState.Deleted;
            db.Tripulantes.Remove(Model);
            db.SaveChanges();
        }

        public void Dispose_()
        {
            db.Dispose();
        }
    }

    public interface ITripulanteDAL
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
