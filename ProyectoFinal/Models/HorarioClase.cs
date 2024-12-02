using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class HorarioClase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Clase")]
        public int ClaseId { get; set; }
        public Clase Clase { get; set; }

        [Required]
        [ForeignKey("Entrenador")]
        public int EntrenadorId { get; set; }
        public Entrenador Entrenador { get; set; }

        [Required]
        [MaxLength(15)]
        public string DiaSemana { get; set; } // Ejemplo: Lunes

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }
    }
}
