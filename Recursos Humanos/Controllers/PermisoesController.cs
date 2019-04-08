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
    public class PermisoesController : Controller
    {
        private Recursos_HumanoEntities db = new Recursos_HumanoEntities();

        // GET: Permisoes
        public ActionResult Index(string busca)
        {
            var permiso = db.Permiso.Include(p => p.Empleados);

            int g = Convert.ToInt32(busca);
            if (busca != null)
            {
                permiso = (from a in db.Permiso where a.ID == g select a);
                return View(permiso.ToList());

            }
            
            return View(permiso.ToList());
        }
        // GET: Permisoes
        public ActionResult PermisosInforme(string busca)
        {
            var permiso = db.Permiso.Include(p => p.Empleados);

            int g = Convert.ToInt32(busca);
            if (busca != null)
            {
                permiso = (from a in db.Permiso where a.ID == g select a);
                return View(permiso.ToList());

            }
            
            return View(permiso.ToList());
        }

        // GET: Permisoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // GET: Permisoes/Create
        public ActionResult Create()
        {
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre");
            return View();
        }

        // POST: Permisoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FK_Empleado,Comentario,Fecha_Entrada,Fecha_Salida")] Permiso permiso)
        {
            int x = Convert.ToInt32(permiso.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Inanctivo";
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                db.Permiso.Add(permiso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", permiso.FK_Empleado);
            return View(permiso);
        }

        // GET: Permisoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", permiso.FK_Empleado);
            return View(permiso);
        }

        // POST: Permisoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FK_Empleado,Comentario,Fecha_Entrada,Fecha_Salida")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permiso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Empleado = new SelectList(db.Empleados, "ID", "Nombre", permiso.FK_Empleado);
            return View(permiso);
        }

        // GET: Permisoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // POST: Permisoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permiso salida = db.Permiso.Find(id);
            int x = Convert.ToInt32(salida.FK_Empleado);
            var q = (from a in db.Empleados where a.ID == x select a).First();
            q.Estatus = "Activo";
            db.Permiso.Remove(salida);
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
