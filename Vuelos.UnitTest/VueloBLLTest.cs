using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.AccesoDatos;
using Vuelos.ReglasNegocio;
using Vuelos.Entidades;

namespace Vuelos.UnitTest
{
    [TestFixture]
    public class VueloBLLTest
    {
        [Test]
        public void CrearTestOK()
        {
            var modelo = new Vuelo();
            modelo.NumeroVuelo = "Vuelo1";
            modelo.VueloID = 1;
            modelo.AerolineaID = 1;
            modelo.FechaVuelo = DateTime.Now;
            modelo.Tripulantes = new List<Tripulante>();
            modelo.CiudadDestino = "Lima";
            modelo.CiudadOrigen = "Arequipa";

            var dal = Substitute.For<IVueloDAL>();
            var bll = new VueloBLL(dal);

            bll.Crear(modelo);
            dal.Received().Crear(modelo);
        }

        [Test]
        public void CrearTestFail()
        {
            var modelo = new Vuelo();
            modelo.NumeroVuelo = "Vuelo1";
            modelo.VueloID = 1;
            modelo.AerolineaID = 1;
            modelo.FechaVuelo = DateTime.Now;
            modelo.Tripulantes = new List<Tripulante>();
            modelo.CiudadDestino = "Lima";
            modelo.CiudadOrigen = "Lima";

            var dal = Substitute.For<IVueloDAL>();
            var bll = new VueloBLL(dal);

            try
            {
                bll.Crear(modelo);
                Assert.Fail("Ciudad y Destinos deben ser diferentes.");
            }
            catch (ApplicationException ex)
            {
                
            }
            
            dal.DidNotReceive().Crear(modelo);
        }
    }
}
