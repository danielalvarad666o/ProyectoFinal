using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal.Models;

namespace ProyectoFinal.Models
{
    public class HorarioClase
    {
        public int Id { get; set; }
        public int ClaseId { get; set; }
        public int EntrenadorId { get; set; }
        public string DiaSemana { get; set; } // Ejemplo: Lunes
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        // Relación con Clase
        public Clase Clase { get; set; }

        // Relación con Entrenador
        public Entrenador Entrenador { get; set; }
    }
}
