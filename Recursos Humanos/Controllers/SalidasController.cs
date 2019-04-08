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
    public class SalidasController : Controller
    {
        private Recursos_HumanoEntities db = new Recursos_HumanoEntities();

        // GET: Salidas
        public ActionResult Index()
        {
            var salida = db.Salida.Include(s => s.Empleados);
            return View(salida.ToList());
        }
        // GET: Salidas
        public ActionResult SalidasInforme()
        {
            var salida = db.Salida.Include(s => s.Empleados);
            return View(salida.ToList());
        }

        // GET: Salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // GET: Salidas/Create
        public ActionResult Create()
        {
            string estado = "Activo";

            ViewBag.FK_Empleado = new SelectList(db.Empleados.Where(s => s.Estatus == estado), "ID", "Nombre");
            return View();
        }

        // POST: Salidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FK_Empleado,Tipo_Salida,Motivo,Dia,Mes,Año")] Salida salida)
        {
            int x = Convert.ToInt32(salida.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Inanctivo";
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                db.Salida.Add(salida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", salida.FK_Empleado);
            return View(salida);
        }

        // GET: Salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", salida.FK_Empleado);
            return View(salida);
        }

        // POST: Salidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FK_Empleado,Tipo_Salida,Motivo,Dia,Mes,Año")] Salida salida)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(salida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", salida.FK_Empleado);
            return View(salida);
        }

        // GET: Salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida salida = db.Salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // POST: Salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salida salida = db.Salida.Find(id);
            int x = Convert.ToInt32(salida.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Activo";
            db.Salida.Remove(salida);
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
