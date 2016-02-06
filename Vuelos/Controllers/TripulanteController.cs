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
using PagedList;

namespace Vuelos.Controllers
{
    public class TripulanteController : Controller
    {
        //private TripulanteBLL tripulanteBLL = new TripulanteBLL();
        //private VueloBLL vueloBLL = new VueloBLL();

        private IVueloBLL vueloBLL;
        private ITripulanteBLL tripulanteBLL;

        public TripulanteController(IVueloBLL pvueloBLL, ITripulanteBLL ptripulanteBLL)
        {
            this.vueloBLL = pvueloBLL;
            this.tripulanteBLL = ptripulanteBLL;
        }

        // GET: /Tripulante/
        public ActionResult Index(Int32? page)
        {
            Int32 paginaTamanio = 5;
            Int32 paginaNumero = (page ?? 1);

            var tripulantes = tripulanteBLL.Listar().ToPagedList(paginaNumero, paginaTamanio);
            return this.View(tripulantes);
        }

        // GET: /Tripulante/Details/5
        public ActionResult Details(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tripulante tripulante = tripulanteBLL.Obtener(Id);
            if (tripulante == null)
            {
                return this.HttpNotFound();
            }

            return View(tripulante);
        }

        // GET: /Tripulante/Create
        public ActionResult Create()
        {
            var vuelos = vueloBLL.Listar(null);
            ViewBag.VueloID = new SelectList(vuelos, "VueloID", "NumeroVuelo");

            return this.View();
        }

        // POST: /Tripulante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TripulanteID,Nombre,Email,VueloID")] Tripulante tripulante)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    tripulanteBLL.Crear(tripulante);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al crear. " + ex.Message);
            }

            var vuelos = vueloBLL.Listar(null);
            ViewBag.VueloID = new SelectList(vuelos, "VueloID", "NumeroVuelo", tripulante.VueloID);
         
            return View(tripulante);
        }

        // GET: /Tripulante/Edit/5
        public ActionResult Edit(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tripulante tripulante = tripulanteBLL.Obtener(Id);
            if (tripulante == null)
            {
                return this.HttpNotFound();
            }

            var vuelos = vueloBLL.Listar(null);
            ViewBag.VueloID = new SelectList(vuelos, "VueloID", "NumeroVuelo", tripulante.VueloID); 
            
            return View(tripulante);
        }

        // POST: /Tripulante/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            String[] camposBind = new String[] { "TripulanteID", "Nombre", "Email", "VueloID" };

            Tripulante tripulanteUpdate = tripulanteBLL.Obtener(Id);

            try
            {
                if (this.TryUpdateModel(tripulanteUpdate, camposBind))
                {
                    tripulanteBLL.Editar(tripulanteUpdate);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al editar. " + ex.Message);
            }

            var vuelos = vueloBLL.Listar(null);
            ViewBag.VueloID = new SelectList(vuelos, "VueloID", "NumeroVuelo", tripulanteUpdate.VueloID);

            return View(tripulanteUpdate);
        }

        // GET: /Tripulante/Delete/5
        public ActionResult Delete(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tripulante tripulante = tripulanteBLL.Obtener(Id);
            if (tripulante == null)
            {
                return this.HttpNotFound();
            }

            return this.View(tripulante);
        }

        // POST: /Tripulante/Delete/5
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tripulante tripulanteDelete = tripulanteBLL.Obtener(Id);

            try
            {
                tripulanteBLL.Eliminar(tripulanteDelete);
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al eliminar. " + ex.Message);
            }

            return this.View(tripulanteDelete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tripulanteBLL.Dispose_();
            }
            base.Dispose(disposing);
        }
    }
}
