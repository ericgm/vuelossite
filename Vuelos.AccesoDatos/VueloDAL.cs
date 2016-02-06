using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;

namespace Vuelos.AccesoDatos
{
    public class VueloDAL : IVueloDAL
    {
        private VueloDBContext db = new VueloDBContext();

        public IEnumerable<Vuelo> Listar(Int32? FiltroAerolinea)
        {
            Int32 aerolineaID = FiltroAerolinea.GetValueOrDefault();

            var vuelos = db.Vuelos.Where(x => (x.AerolineaID == aerolineaID || !FiltroAerolinea.HasValue))
                                  .Include(v => v.Aerolinea);

            return vuelos.ToList();
        }

        public Vuelo Obtener(Int32? Id)
        {
            return db.Vuelos.Find(Id);
        }

        public void Crear(Vuelo Model)
        {
            db.Vuelos.Add(Model);
            db.SaveChanges();
        }

        public void Editar(Vuelo Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(Vuelo Model)
        {
            db.Entry(Model).State = EntityState.Deleted;
            db.Vuelos.Remove(Model);
            db.SaveChanges();
        }

        public void Dispose_()
        {
            db.Dispose();
        }
    }

    public interface IVueloDAL
    {
        IEnumerable<Vuelo> Listar(Int32? FiltroAerolinea);
        Vuelo Obtener(Int32? Id);
        void Crear(Vuelo Model);
        void Editar(Vuelo Model);
        void Eliminar(Vuelo Model);
        void Dispose_();
    }
}
