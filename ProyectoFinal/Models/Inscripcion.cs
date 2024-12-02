using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Inscripcion
    {
        [Key]
        public int Id { get; set; } // Clave primaria

        [Required]
        [ForeignKey("Usuario")]
        public int UserId { get; set; }
        public User Usuario { get; set; } // Navegación hacia User

        [Required]
        [ForeignKey("Clase")]
        public int ClaseId { get; set; }
        public Clase Clase { get; set; } // Navegación hacia Clase

        [Required]
        public DateTime FechaInscripcion { get; set; } // Fecha de inscripción
    }
}


