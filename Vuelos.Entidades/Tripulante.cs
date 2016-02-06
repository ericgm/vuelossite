using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Entidades
{
    public class Tripulante
    {
        public Int32 TripulanteID { get; set; }
        
        [StringLength(100)]
        public String Nombre { get; set; }
        
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        
        public Int32 VueloID { get; set; }
        
        public virtual Vuelo Vuelo { get; set; }
        public virtual ICollection<Aerolinea> Aerolineas { get; set; }
    }
}
