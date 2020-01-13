using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index(){

            //Conexion BD desde modelo
            AlumnosContext db = new AlumnosContext();

            //Listar - a=> Expresión lambda
            //Uso de LinQ
            //List<Alumnos> listaAlumnos = db.Alumnos.Where(a => a.Edad > 18).ToList();

            return View(db.Alumnos.ToList());
        }

        [HttpGet]
        public ActionResult Agregar() {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Alumnos alumnos) {
            return View();
        }

    }
}