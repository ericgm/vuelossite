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
using Vuelos.AccesoDatos;
using Vuelos.ViewModels;

namespace Vuelos.Controllers
{
    public class AerolineaController : Controller
    {
        //private AerolineaBLL aerolineaBLL = new AerolineaBLL();
        //private TripulanteBLL tripulanteBLL = new TripulanteBLL();
        private VueloDBContext db = new VueloDBContext();
        private IAerolineaBLL aerolineaBLL;
        private ITripulanteBLL tripulanteBLL;

        public AerolineaController(IAerolineaBLL paerolineaBLL,ITripulanteBLL ptripulanteBLL)
        {
            this.aerolineaBLL = paerolineaBLL;
            this.tripulanteBLL = ptripulanteBLL;
        }

        // GET: /Aerolinea/
        public ActionResult Index(Int32? Id)
        {
            AerolineaData model = new AerolineaData();
            model.Aerolineas = aerolineaBLL.Listar();

            if (Id != null)
            {
                ViewBag.AerolineaID = Id;
                model.Vuelos = model.Aerolineas.Where(a => a.AerolineaID == Id).Single().Vuelos;
                model.Tripulantes = model.Aerolineas.Where(a => a.AerolineaID == Id).Single().Tripulantes;
            }

            return this.View(model);
        }

        // GET: /Aerolinea/Details/5
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aerolinea aerolinea = aerolineaBLL.Obtener(Id);
            if (aerolinea == null)
            {
                return this.HttpNotFound();
            }
            return this.View(aerolinea);
        }

        // GET: /Aerolinea/Create
        public ActionResult Create()
        {
            Aerolinea aerolinea = new Aerolinea();
            aerolinea.Tripulantes = new List<Tripulante>();
            this.AsignarTripulantes(aerolinea);
            return View();

            //Aerolinea aerolinea = new Aerolinea();
            //aerolinea.Tripulantes = new List<Tripulante>();
            //this.AsignarTripulantes(aerolinea);

            //return this.View();
        }

        // POST: /Aerolinea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AerolineaID,NombreComercial,FechaCreacion,Director")] Aerolinea aerolinea, String[] tripulantesSeleccionados)
        {
            if (tripulantesSeleccionados != null)
            {
                aerolinea.Tripulantes = new List<Tripulante>();
                foreach (var tripulante in tripulantesSeleccionados)
                {
                    var tripulanteCreate = db.Tripulantes.Find(Int32.Parse(tripulante));
                    aerolinea.Tripulantes.Add(tripulanteCreate);
                }
            }

            if (ModelState.IsValid)
            {
                db.Aerolineas.Add(aerolinea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.AsignarTripulantes(aerolinea);
            return View(aerolinea);

            //if (tripulantesSeleccionados != null)
            //{
            //    aerolinea.Tripulantes = new List<Tripulante>();
            //    foreach (var tripulante in tripulantesSeleccionados)
            //    {
            //        var tripulanteCreate = tripulanteBLL.Obtener(Convert.ToInt32(tripulante));
            //        aerolinea.Tripulantes.Add(tripulanteCreate);
            //    }
            //}

            //try
            //{
            //    if (this.ModelState.IsValid)
            //    {
            //        aerolineaBLL.Crear(aerolinea);
            //        return this.RedirectToAction("Index");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ModelState.AddModelError("", "Error al crear. " + ex.Message);
            //}

            //this.AsignarTripulantes(aerolinea);
            //return this.View(aerolinea);
        }

        // GET: /Aerolinea/Edit/5
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aerolinea aerolinea = db.Aerolineas.Include(a => a.Tripulantes)
                                                  .Where(i => i.AerolineaID == Id)
                                                  .Single();
            if (aerolinea == null)
            {
                return HttpNotFound();
            }

            this.AsignarTripulantes(aerolinea);
            return View(aerolinea);

            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Aerolinea aerolinea = aerolineaBLL.ObtenerCompleto(Id);
            //if (aerolinea == null)
            //{
            //    return this.HttpNotFound();
            //}

            //this.AsignarTripulantes(aerolinea);
            //return this.View(aerolinea);
        }

        // POST: /Aerolinea/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Int32? Id, String[] tripulantesSeleccionados)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aerolineaUpdate = db.Aerolineas.Include(i => i.Tripulantes)
                                                   .Where(i => i.AerolineaID == Id)
                                                   .Single();

            if (TryUpdateModel(aerolineaUpdate, "", new String[] { "AerolineaID", "NombreComercial", "FechaCreacion", "Director" }))
            {
                try
                {
                    this.ActualizarAerolineaTripulante(aerolineaUpdate, tripulantesSeleccionados);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception dex)
                {
                    ModelState.AddModelError("", dex.Message + " - Unable to save changes.");
                }
            }

            this.AsignarTripulantes(aerolineaUpdate);
            return View(aerolineaUpdate);

            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //String[] camposBind = new String[] { "AerolineaID", "NombreComercial", "FechaCreacion", "Director" };

            //Aerolinea aerolineaUpdate = aerolineaBLL.ObtenerCompleto(Id);
            
            //try
            //{
            //    if (this.TryUpdateModel(aerolineaUpdate, camposBind))
            //    {
            //        this.ActualizarAerolineaTripulante(aerolineaUpdate, tripulantesSeleccionados);

            //        aerolineaBLL.Editar(aerolineaUpdate);
            //        return this.RedirectToAction("Index");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ModelState.AddModelError("", "Error al editar. " + ex.Message);
            //}

            //this.AsignarTripulantes(aerolineaUpdate);
            //return this.View(aerolineaUpdate);
        }

        // GET: /Aerolinea/Delete/5
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aerolinea aerolinea = aerolineaBLL.Obtener(Id);
            if (aerolinea == null)
            {
                return this.HttpNotFound();
            }
            return this.View(aerolinea);
        }

        // POST: /Aerolinea/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Int32? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aerolinea aerolineaDelete = aerolineaBLL.Obtener(Id);

            try
            {
                aerolineaBLL.Eliminar(aerolineaDelete);
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", "Error al eliminar. " + ex.Message);
            }

            return this.View(aerolineaDelete);            
        }

        public ActionResult Resumen()
        {
            IQueryable<VuelosGroup> registros = from vuelos in db.Vuelos
                                                group vuelos by vuelos.Aerolinea.NombreComercial into grupoVuelo
                                                select new VuelosGroup() 
                                                {
                                                    Aerolinea = grupoVuelo.Key,
                                                    TotalVuelos = grupoVuelo.Count()
                                                };

            return this.View(registros.ToList());
        }

        private void AsignarTripulantes(Aerolinea aerolinea)
        {
            var listaTripulantes = db.Tripulantes;
            var aerolineaTripulante = new HashSet<Int32>(aerolinea.Tripulantes.Select(c => c.TripulanteID));
            var model = new List<TripulanteLista>();

            foreach (var tripulante in listaTripulantes)
            {
                model.Add(new TripulanteLista
                {
                    TripulanteID = tripulante.TripulanteID,
                    Nombre = tripulante.Nombre,
                    Seleccionado = aerolineaTripulante.Contains(tripulante.TripulanteID)
                });
            }

            ViewBag.Tripulantes = model;

            //var tripulantes = tripulanteBLL.ListarTodos();
            //var aerolineaTripulante = new HashSet<Int32>(aerolinea.Tripulantes.Select(t => t.TripulanteID));
            //var model = new List<TripulanteLista>();

            //foreach (var item in tripulantes)
            //{
            //    model.Add(new TripulanteLista
            //    {
            //        TripulanteID = item.TripulanteID, 
            //        Nombre = item.Nombre, 
            //        Seleccionado = aerolineaTripulante.Contains(item.TripulanteID)
            //    });
            //}

            //ViewBag.Tripulantes = model;
        }

        private void ActualizarAerolineaTripulante(Aerolinea aerolineaUpdate, String[] tripulantesSeleccionados)
        {
            if (tripulantesSeleccionados == null)
            {
                aerolineaUpdate.Tripulantes = new List<Tripulante>();
                return;
            }

            var tripulantesSeleccionadosHS = new HashSet<String>(tripulantesSeleccionados);
            var aerolineaTripulante = new HashSet<Int32>(aerolineaUpdate.Tripulantes.Select(c => c.TripulanteID));

            foreach (var tripulante in db.Tripulantes)
            {
                if (tripulantesSeleccionadosHS.Contains(tripulante.TripulanteID.ToString()))
                {
                    // Tripulante sí marcado
                    if (!aerolineaTripulante.Contains(tripulante.TripulanteID))
                    {
                        // Aerolinea sin el tripulante lo adiciona.
                        aerolineaUpdate.Tripulantes.Add(tripulante);
                    }
                }
                else
                {
                    // Tripulante no marcado
                    if (aerolineaTripulante.Contains(tripulante.TripulanteID))
                    {
                        // Aerolinea con el tripulante lo elimina.
                        aerolineaUpdate.Tripulantes.Remove(tripulante);
                    }
                }
            }

            //if (tripulantesSeleccionados == null)
            //{
            //    aerolineaUpdate.Tripulantes = new List<Tripulante>();
            //    return;
            //}

            //var tripulantesSeleccionadosHS = new HashSet<String>(tripulantesSeleccionados);
            //var aerolineaTripulante = new HashSet<Int32>(aerolineaUpdate.Tripulantes.Select(t => t.TripulanteID));
            //var tripulantes = tripulanteBLL.ListarTodos();

            //foreach (var item in tripulantes)
            //{
            //    if (tripulantesSeleccionadosHS.Contains(item.TripulanteID.ToString()))
            //    {
            //        // Tripulante sí marcado
            //        if (!aerolineaTripulante.Contains(item.TripulanteID))
            //        {
            //            // Aerolinea sin el tripulante lo adiciona.
            //            aerolineaUpdate.Tripulantes.Add(item);
            //        }
            //    }
            //    else
            //    {
            //        // Tripulante no marcado
            //        if (aerolineaTripulante.Contains(item.TripulanteID))
            //        {
            //            // Aerolinea con el tripulante lo elimina.
            //            aerolineaUpdate.Tripulantes.Remove(item);
            //        }
            //    }
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                aerolineaBLL.Dispose_();
            }
            base.Dispose(disposing);
        }
    }
}
