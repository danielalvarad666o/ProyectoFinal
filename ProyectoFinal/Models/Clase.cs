using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Clase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public int DuracionMin { get; set; } // Duración en minutos

        [Required]
        public int MaxParticipantes { get; set; }

        // Relación uno a muchos con Inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        // Relación uno a muchos con HorarioClase
        public ICollection<HorarioClase> HorariosClases { get; set; } = new List<HorarioClase>();
    }
}

