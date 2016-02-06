using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;

namespace Vuelos.AccesoDatos
{
    public class CiudadDAL : ICiudadDAL
    {
        private VueloDBContext db = new VueloDBContext();

        public IEnumerable<Ciudad> Listar()
        {
            return db.Ciudades.ToList();
        }

        public Ciudad Obtener(String Id)
        {
            return db.Ciudades.Find(Id);
        }

        public IList<Ciudad> Buscar(String filtro)
        {
            var datos = from c in db.Ciudades
                        where c.Nombre.Contains(filtro)
                        select c;

            return datos.ToList();
        }

        public void Crear(Ciudad Model)
        {
            db.Ciudades.Add(Model);
            db.SaveChanges();
        }

        public void Editar(Ciudad Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(Ciudad Model)
        {
            db.Entry(Model).State = EntityState.Deleted;
            db.Ciudades.Remove(Model);
            db.SaveChanges();
        }

        public void Dispose_()
        {
            db.Dispose();
        }
    }

    public interface ICiudadDAL
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
