using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Recursos_Humanos.Models;

namespace Recursos_Humanos.Controllers
{
    public class LicenciasController : Controller
    {
        private Recursos_HumanoEntities db = new Recursos_HumanoEntities();

        // GET: Licencias
        public ActionResult Index()
        {
            var licencias = db.Licencias.Include(l => l.Empleados);
            return View(licencias.ToList());
        }
        // GET: Licencias
        public ActionResult LicenciasInforme()
        {
            var licencias = db.Licencias.Include(l => l.Empleados);
            return View(licencias.ToList());
        }

        // GET: Licencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencias licencias = db.Licencias.Find(id);
            if (licencias == null)
            {
                return HttpNotFound();
            }
            return View(licencias);
        }

        // GET: Licencias/Create
        public ActionResult Create()
        {
            var activo = "Activo";

            ViewBag.FK_Empleado = new SelectList(db.Empleados.Where(m=>m.Estatus == activo), "ID", "Nombre");
            return View();
        }

        // POST: Licencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FK_Empleado,Comentario,Fecha_Entrada,Fecha_Salida")] Licencias licencias)
        {
            int x = Convert.ToInt32(licencias.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Inanctivo";
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                db.Licencias.Add(licencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", licencias.FK_Empleado);
            return View(licencias);
        }

        // GET: Licencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencias licencias = db.Licencias.Find(id);
            if (licencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", licencias.FK_Empleado);
            return View(licencias);
        }

        // POST: Licencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FK_Empleado,Comentario,Fecha_Entrada,Fecha_Salida")] Licencias licencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", licencias.FK_Empleado);
            return View(licencias);
        }

        // GET: Licencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencias licencias = db.Licencias.Find(id);
            if (licencias == null)
            {
                return HttpNotFound();
            }
            return View(licencias);
        }

        // POST: Licencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Licencias salida = db.Licencias.Find(id);
            int x = Convert.ToInt32(salida.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Activo";
            db.Licencias.Remove(salida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
