using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models {
    public class AlumnoCE {
        //DataAnnotations - Validación en los campos de entrada
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
    }

    [MetadataType(typeof(AlumnoCE))]

    public partial class Alumnos {
        //Usar clase parciale para agregar nuevo metodo a la clase Alumnos
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }

    }

}