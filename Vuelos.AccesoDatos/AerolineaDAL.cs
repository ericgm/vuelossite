using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;

namespace Vuelos.AccesoDatos
{
    public class AerolineaDAL : IAerolineaDAL
    {
        private VueloDBContext db = new VueloDBContext();

        public IEnumerable<Aerolinea> Listar()
        {
            var aerolineas = db.Aerolineas.Include(a => a.Vuelos)
                                          .OrderBy(x => x.NombreComercial);

            return aerolineas.ToList();
        }

        public Aerolinea Obtener(Int32? Id)
        {
            return db.Aerolineas.Find(Id);
        }

        public Aerolinea ObtenerCompleto(Int32? Id)
        {
            var aerolinea = db.Aerolineas.Include(a => a.Tripulantes)
                                         .Where(x => x.AerolineaID == Id)
                                         .Single();

            return aerolinea;
        }

        public void Crear(Aerolinea Model)
        {
            db.Aerolineas.Add(Model);
            db.SaveChanges();
        }

        public void Editar(Aerolinea Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(Aerolinea Model)
        {
            db.Entry(Model).State = EntityState.Deleted;
            db.Aerolineas.Remove(Model);
            db.SaveChanges();
        }

        public void Dispose_()
        {
            db.Dispose();
        }
    }

    public interface IAerolineaDAL
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
