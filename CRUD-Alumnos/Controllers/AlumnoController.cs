using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index(){
            try {
                /*int edad = 18; //para condicion de edad - simula un parametro que llega al metodo
                string sql = @"
                        select a.Id, a.Nombre, a.Apellidos, a.Edad, a.Sexo, a.FechaRegistro, c.Nombre as NombreCiudad 
                        from Alumnos a 
                        inner join Ciudad c on a.CodCiudad = c.Id
                        where a.edad > @edad";*/
                //Conexion BD desde modelo
                using (var db = new AlumnosContext()) {

                    //Listar - a=> Expresión lambda
                    //Uso de LinQ

                    //Mostrar datos de varias tablas
                    var data = from a in db.Alumnos
                               join c in db.Ciudad on a.CodCiudad equals c.Id
                               select new AlumnoCE() {
                                   Id = a.Id,
                                   Nombre = a.Nombre,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   FechaRegistro = a.FechaRegistro,
                                   NombreCiudad = c.Nombre
                               };
                    /*
                    var data = from a in db.Alumnos
                               join c in db.Ciudad on a.CodCiudad equals c.Id
                               where a.Edad > 18
                               select a;
                    */
                    //List<Alumnos> listaAlumnos = db.Alumnos.Where(a => a.Edad > 18).ToList();

                    return View(data.ToList());
                    //return View(db.Alumnos.ToList());
                    //return View(db.Database.SqlQuery<AlumnoCE>(sql, new SqlParameter("@edad", edad)).ToList());
                }
            } catch (Exception error ) {
                 MessageBox.Show(error.Message.ToString());
                return View();


            }
            

            
        }
        //Get Alumnos
        [HttpGet]
        public ActionResult Agregar() {
            return View();
        }

        //Post Alumnos - Agregar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumnos alumnos) {

            //Modelo Valido -> alumnos
            if (!ModelState.IsValid) {
                return View();
            }

            try {
                
                using (AlumnosContext db = new AlumnosContext()) {//usar using para no dejar la conexion abierta

                    //Asignar fecha actual al atributo fechaRegistro del objeto alumnos
                    alumnos.FechaRegistro = DateTime.Now;

                    db.Alumnos.Add(alumnos);//Adicionar alumnos
                    db.SaveChanges();//Guardar cambios

                    return RedirectToAction("Index");
                }
            } catch (Exception error) {
                //ModelState.AddModelError("", "Error al registrar Alumno -" + error.Message);
                ModelState.AddModelError("", "Error al registrar Alumno");
                return View();
            }

            
            
        }

        //Lista Ciudades
        public ActionResult ListaCiudades() {
            using(var db = new AlumnosContext()) {

                return PartialView(db.Ciudad.ToList());
            }
        }


        //Get Editar
        [HttpGet]
        public ActionResult Editar(int id) {
            try {
                using (var db = new AlumnosContext()) {

                    //Alumnos alumno = db.Alumnos.Where(a => a.Id == id).FirstOrDefault();
                    Alumnos alumnos = db.Alumnos.Find(id);//id debe ser unico, sino usar expresion de arriba
                    return View(alumnos);
                }
            } catch (Exception error) {
                    ModelState.AddModelError("", "Error al editar Alumno");
                    return View();
            }
 
        }

        //Post Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumnos alumnoForm) {

            if (!ModelState.IsValid) {
                return View();
            }
            try {//using -> evitar dejar conexiones abiertas

                using (var db = new AlumnosContext()) {

                    Alumnos alumnoDB = db.Alumnos.Find(alumnoForm.Id);//Buscar id
                    //Editar
                    alumnoDB.Nombre = alumnoForm.Nombre;
                    alumnoDB.Apellidos = alumnoForm.Apellidos;
                    alumnoDB.Edad = alumnoForm.Edad;
                    alumnoDB.Sexo = alumnoForm.Sexo;

                    //Guardar y Redireccionar
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            } catch (Exception error) {
                    ModelState.AddModelError("", "Error al editar Alumno");
                    return View();
            }
   
        }

        public ActionResult DetallesAlumno(int id) {
            try {
                using (var db = new AlumnosContext()) {

                    
                    Alumnos alumnos = db.Alumnos.Find(id);
                    return View(alumnos);

                }
            } catch (Exception error) {
                ModelState.AddModelError("", "Error al editar Alumno");
                return View();
            }
        }

        public ActionResult EliminarAlumno(int id) {
            try {
                using (var db = new AlumnosContext()) {


                    Alumnos alumnoForm = db.Alumnos.Find(id);
                    db.Alumnos.Remove(alumnoForm);
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
            } catch (Exception error) {
                ModelState.AddModelError("", "Error al editar Alumno");
                return View();
            }
        }

        public static string NombreCiudad(int CodCiudad) {

            using (var db = new AlumnosContext()) {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }

    }
}