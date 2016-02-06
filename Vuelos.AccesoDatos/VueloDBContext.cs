using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Entidades;

namespace Vuelos.AccesoDatos
{
    public class VueloDBContext : DbContext
    {
        public VueloDBContext()
            : base("DBVuelos")
        {
        }

        public DbSet<Aerolinea> Aerolineas { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Tripulante> Tripulantes { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Aerolinea>()
                        .HasMany(a => a.Tripulantes).WithMany(t => t.Aerolineas)
                        .Map(m => m.MapLeftKey("AerolineaID").MapRightKey("TripulanteID").ToTable("Tripulacion"));
        }
    }
}
