using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMin { get; set; } // Duración en minutos
        public int MaxParticipantes { get; set; }

        // Relación uno a muchos con HorarioClase
        public ICollection<HorarioClase> HorariosClases { get; set; }
    }
}
