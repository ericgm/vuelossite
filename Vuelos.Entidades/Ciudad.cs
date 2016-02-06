using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Entidades
{
    public class Ciudad
    {
        [Display(Name = "Codigo")]
        [StringLength(3)]
        public String CiudadID { get; set; }

        [StringLength(100)]
        public String Nombre { get; set; }
    }
}
