using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Entidades
{
    public class Vuelo
    {
        public Int32 VueloID { get; set; }

        [Display(Name = "Nro. Vuelo")]
        [StringLength(10)]
        public String NumeroVuelo { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaVuelo { get; set; }

        [Display(Name = "Origen")]
        [StringLength(3)]
        public String CiudadOrigen { get; set; }

        [Display(Name = "Destino")]
        [StringLength(3)]
        public String CiudadDestino { get; set; }

        public Int32 AerolineaID { get; set; }

        public virtual ICollection<Tripulante> Tripulantes { get; set; }
        public virtual Aerolinea Aerolinea { get; set; }
    }
}
