using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vuelos.Entidades;
using Vuelos.ReglasNegocio;

namespace Vuelos.Controllers
{
    public class CiudadController : Controller
    {
        //private CiudadBLL ciudadBLL = new CiudadBLL();

        private ICiudadBLL ciudadBLL;

        public CiudadController(ICiudadBLL pciudadBLL)
        {
            this.ciudadBLL = pciudadBLL;
        }

        // GET: /Ciudad/
        public ActionResult Index()
        {
            var ciudades = ciudadBLL.Listar();
            return this.View(ciudades);
        }

        [HttpPost]
        public ActionResult Busqueda(string filtro)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return this.HttpNotFound("this action is not valid with ajax request");
            }

            var datos = ciudadBLL.Buscar(filtro);
            var ciudades = new { items = new List<Object>() };
            foreach (var item in datos)
            {
                ciudades.items.Add(new { Codigo = item.CiudadID, Ciudad = item.Nombre });
            }
            
            return this.Json(ciudades, JsonRequestBehavior.DenyGet);
        }

        // GET: /Ciudad/Details/CIX
        public ActionResult Details(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ciudad ciudad = ciudadBLL.Obtener(Id);
            if (ciudad == null)
            {
                return this.HttpNotFound();
            }

            return this.View(ciudad);
        }

        // GET: /Ciudad/Create
        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: /Ciudad/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "CiudadID,Nombre")] Ciudad Model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    ciudadBLL.Crear(Model);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al crear. " + ex.Message);
            }

            return this.View(Model);
        }

        // GET: /Ciudad/Edit/CIX
        [Authorize]
        public ActionResult Edit(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ciudad ciudad = ciudadBLL.Obtener(Id);
            if (ciudad == null)
            {
                return this.HttpNotFound();
            }

            return this.View(ciudad);
        }

        // POST: /Ciudad/Edit/CIX
        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditPost(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            String[] camposBind = new String[] { "CiudadID", "Nombre" };

            Ciudad ciudadUpdate = ciudadBLL.Obtener(Id);

            try
            {
                if (this.TryUpdateModel(ciudadUpdate, camposBind))
                {
                    ciudadBLL.Editar(ciudadUpdate);
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al editar. " + ex.Message);
            }

            return this.View(ciudadUpdate);
        }

        // GET: Ciudad/Delete/CIX
        [Authorize]
        public ActionResult Delete(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ciudad ciudad = ciudadBLL.Obtener(Id);
            if (ciudad == null)
            {
                return this.HttpNotFound();
            }

            return this.View(ciudad);
        }

        // POST: /Ciudad/Delete/CIX
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeletePost(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ciudad ciudadDelete = ciudadBLL.Obtener(Id);

            try
            {
                ciudadBLL.Eliminar(ciudadDelete);
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al eliminar. " + ex.Message);
            }

            return this.View(ciudadDelete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ciudadBLL.Dispose_();
            }
            base.Dispose(disposing);
        }
    }
}
