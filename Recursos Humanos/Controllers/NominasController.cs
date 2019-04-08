using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Recursos_Humanos.Models;

namespace Recursos_Humanos.Controllers
{
    public class NominasController : Controller
    {
        private Recursos_HumanoEntities db = new Recursos_HumanoEntities();

        // GET: Nominas
        public ActionResult Index(string busca)
        {
            var empleados = (from a in db.Nomina select a);
            if (busca != null)
            {
                empleados = (from a in db.Nomina where a.Año == busca || a.Mes == busca select a);

            }
            return View(empleados.ToList());
           
        }

        public JsonResult ChartFunction()
        {
            string query = "select Año,Monto_Total from Nomina";
            List<Nomina> chartData = new List<Nomina>();
            using (SqlConnection con = new SqlConnection(@"data source=MSI;initial catalog=Recursos_Humano;
            Integrated security = True"))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Nomina pd = new Nomina();
                            pd.Año = dr["Año"].ToString();
                            pd.Monto_Total = Convert.ToInt32(dr["Monto_Total"]);
                            chartData.Add(pd);
                        }
                    }
                    con.Close();
                }
            }
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NominaInforme(string busca)
        {
            var empleados = (from a in db.Nomina select a);
            if (busca != null)
            {
                empleados = (from a in db.Nomina where a.Año == busca || a.Mes == busca select a);

            }
            return View(empleados.ToList());
        }

        // GET: Nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // GET: Nominas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nominas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Monto_Total,Dia,Mes,Año")] Nomina nomina)
        {
            Nullable<Decimal> total =
                (from ord in db.Empleados
                 select ord.Salario)
                .Sum();

            if (ModelState.IsValid)
            {
                db.Nomina.Add(nomina);
                nomina.Monto_Total = total;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Monto_Total,Dia,Mes,Año")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nomina);
        }

        // GET: Nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nomina nomina = db.Nomina.Find(id);
            db.Nomina.Remove(nomina);
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
