using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vuelos.ViewModels
{
    public class TripulanteLista
    {
        public Int32 TripulanteID { get; set; }

        public String Nombre { get; set; }

        public Boolean Seleccionado { get; set; }
    }
}