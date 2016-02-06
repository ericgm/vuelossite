using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Entidades
{
    public class Aerolinea
    {
        public Int32 AerolineaID { get; set; }

        [Display(Name = "Aerolinea")]
        [StringLength(100)]
        public String NombreComercial { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

        [StringLength(100)]
        public String Director { get; set; }

        public virtual ICollection<Vuelo> Vuelos { get; set; }
        public virtual ICollection<Tripulante> Tripulantes { get; set; }
    }
}
