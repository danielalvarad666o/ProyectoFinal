using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal.Models;

namespace ProyectoFinal.Models
{
    public class Entrenador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaContratacion { get; set; }
        public bool Estado { get; set; }

        // Relación uno a muchos con HorarioClase
        public ICollection<HorarioClase> HorariosClases { get; set; }
    }
}
