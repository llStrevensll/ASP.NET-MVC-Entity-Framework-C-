using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models {
    public class AlumnoCE {
        //DataAnnotations - Validación en los campos de entrada

        public int Id { get; set; }

        [Required]
        [Display(Name = "Ingrese Nombres")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Ingrese Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Ingrese Edad")]
        public int Edad { get; set; }

        [Required]
        [Display(Name = "Ingrese Sexo")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Ingrese Ciudad")]
        public int CodCiudad { get; set; }

        public System.DateTime FechaRegistro { get; set; }

        public string NombreCiudad { get; set; }

        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }
    }

    [MetadataType(typeof(AlumnoCE))]

    public partial class Alumnos {
        //Usar clase parcial para agregar nuevo metodo a la clase Alumnos
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }

        public string NombreCiudad { get; set; }

    }

}