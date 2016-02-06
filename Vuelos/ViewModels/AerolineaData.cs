using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vuelos.Entidades;

namespace Vuelos.ViewModels
{
    public class AerolineaData
    {
        public IEnumerable<Aerolinea> Aerolineas { get; set; }
        public IEnumerable<Vuelo> Vuelos { get; set; }
        public IEnumerable<Tripulante> Tripulantes { get; set; }
    }
}