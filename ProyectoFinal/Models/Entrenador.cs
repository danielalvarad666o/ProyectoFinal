using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Entrenador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public DateTime FechaContratacion { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Relación uno a muchos con HorarioClase
        public ICollection<HorarioClase> HorariosClases { get; set; } = new List<HorarioClase>();
    }
}
