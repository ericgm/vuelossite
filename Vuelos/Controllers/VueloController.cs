using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vuelos.Entidades;
using Vuelos.ReglasNegocio;

namespace Vuelos.Controllers
{
    public class VueloController : Controller
    {
        //private VueloBLL vueloBLL = new VueloBLL();
        //private AerolineaBLL aerolineaBLL = new AerolineaBLL();
        //private CiudadBLL ciudadBLL = new CiudadBLL();

        private IVueloBLL vueloBLL;
        private IAerolineaBLL aerolineaBLL;
        private ICiudadBLL ciudadBLL;

        public VueloController(IVueloBLL pvueloBLL, IAerolineaBLL paerolineaBLL, ICiudadBLL pciudadBLL)
        {
            this.vueloBLL = pvueloBLL;
            this.aerolineaBLL = paerolineaBLL;
            this.ciudadBLL = pciudadBLL;
        }

        // GET: /Vuelo/
        public ActionResult Index(Int32? FiltroAerolinea)
        {
            var aerolineas = aerolineaBLL.Listar().OrderBy(a => a.NombreComercial);
            ViewBag.FiltroAerolinea = new SelectList(aerolineas, "AerolineaID", "NombreComercial", FiltroAerolinea);

            var vuelos = vueloBLL.Listar(FiltroAerolinea);
            return this.View(vuelos);
        }

        // GET: /Vuelo/Details/5
        public ActionResult Details(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vuelo vuelo = vueloBLL.Obtener(Id);
            if (vuelo == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vuelo);
        }

        // GET: /Vuelo/Create
        public ActionResult Create()
        {
            var aerolineas = aerolineaBLL.Listar();
            var ciudades = ciudadBLL.Listar();

            ViewBag.AerolineaID = new SelectList(aerolineas, "AerolineaID", "NombreComercial");
            ViewBag.CiudadOrigen = new SelectList(ciudades, "CiudadID", "Nombre");
            ViewBag.CiudadDestino = new SelectList(ciudades, "CiudadID", "Nombre");
            
            return this.View();
        }

        // POST: /Vuelo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VueloID,NumeroVuelo,FechaVuelo,CiudadOrigen,CiudadDestino,AerolineaID")] Vuelo vuelo)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    vueloBLL.Crear(vuelo);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al crear. " + ex.Message);
            }

            var aerolineas = aerolineaBLL.Listar();
            var ciudades = ciudadBLL.Listar();

            ViewBag.AerolineaID = new SelectList(aerolineas, "AerolineaID", "NombreComercial", vuelo.AerolineaID);
            ViewBag.CiudadOrigen = new SelectList(ciudades, "CiudadID", "Nombre", vuelo.CiudadOrigen);
            ViewBag.CiudadDestino = new SelectList(ciudades, "CiudadID", "Nombre", vuelo.CiudadDestino);
            
            return this.View(vuelo);
        }

        // GET: /Vuelo/Edit/5
        public ActionResult Edit(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vuelo vuelo = vueloBLL.Obtener(Id);
            if (vuelo == null)
            {
                return this.HttpNotFound();
            }

            var aerolineas = aerolineaBLL.Listar();
            var ciudades = ciudadBLL.Listar();

            ViewBag.AerolineaID = new SelectList(aerolineas, "AerolineaID", "NombreComercial", vuelo.AerolineaID);
            ViewBag.CiudadOrigen = new SelectList(ciudades, "CiudadID", "Nombre", vuelo.CiudadOrigen);
            ViewBag.CiudadDestino = new SelectList(ciudades, "CiudadID", "Nombre", vuelo.CiudadDestino);

            return this.View(vuelo);
        }

        // POST: /Vuelo/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            String[] camposBind = new String[] { "VueloID", "NumeroVuelo", "FechaVuelo", "CiudadOrigen", "CiudadDestino", "AerolineaID" };

            Vuelo vueloUpdate = vueloBLL.Obtener(Id);
            
            try
            {
                if (this.TryUpdateModel(vueloUpdate, camposBind))
                {
                    vueloBLL.Editar(vueloUpdate);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al editar. " + ex.Message);
            }
            
            var aerolineas = aerolineaBLL.Listar();
            var ciudades = ciudadBLL.Listar();

            ViewBag.AerolineaID = new SelectList(aerolineas, "AerolineaID", "NombreComercial", vueloUpdate.AerolineaID);
            ViewBag.CiudadOrigen = new SelectList(ciudades, "CiudadID", "Nombre", vueloUpdate.CiudadOrigen);
            ViewBag.CiudadDestino = new SelectList(ciudades, "CiudadID", "Nombre", vueloUpdate.CiudadDestino);

            return this.View(vueloUpdate);
        }

        // GET: /Vuelo/Delete/5
        public ActionResult Delete(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vuelo vuelo = vueloBLL.Obtener(Id);
            if (vuelo == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vuelo);
        }

        // POST: /Vuelo/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vuelo vueloDelete = vueloBLL.Obtener(Id);

            try
            {
                vueloBLL.Eliminar(vueloDelete);
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al eliminar. " + ex.Message);
            }

            return this.View(vueloDelete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                vueloBLL.Dispose_();
            }
            base.Dispose(disposing);
        }
    }
}
