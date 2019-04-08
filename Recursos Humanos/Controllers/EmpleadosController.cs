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
    public class EmpleadosController : Controller
    {
        private Recursos_HumanoEntities db = new Recursos_HumanoEntities();

        // GET: Empleados
        public ActionResult Index(string busca)
        {
            var empleados = (from a in db.Empleados where a.Estatus == "Activo" select a);
            var l = from a in db.Empleados select a;
            DateTime f = Convert.ToDateTime(busca);
          
            if (busca != null)
            {
                empleados = (from a in db.Empleados where a.Nombre == busca && a.Estatus=="Activo" || a.Fecha_DE_Ingreso==f select a);
              
            }
            return View(empleados.ToList());
        }
        // GET: Empleados
        public ActionResult Inactivo(string busco)
        {
            var empleados = (from a in db.Empleados where a.Estatus == "Inanctivo"  select a);
            var l = from a in db.Empleados select a;
            DateTime f = Convert.ToDateTime(busco);
            if (busco != null)
            {
                empleados = (from a in db.Empleados where a.Nombre == busco && a.Estatus == "Inanctivo" || a.Fecha_DE_Ingreso == f select a);
            }
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1");
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codigo_Empleado,Departamento,Nombre,Apellido,Cargo,Salario,Fecha_DE_Ingreso,Telefono,Estatus")] Empleados empleados)
        {
            
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                empleados.Estatus = "Activo";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codigo_Empleado,Departamento,Nombre,Apellido,Cargo,Salario,Fecha_DE_Ingreso,Telefono,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                empleados.Estatus = "Activo";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
