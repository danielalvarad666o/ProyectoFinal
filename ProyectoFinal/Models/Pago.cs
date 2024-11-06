using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{

    public class Pago
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MembresiaId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } // Ejemplo: Tarjeta, Efectivo

        // Relación con Usuario
        public User User { get; set; }

        // Relación con Membresia
        public Membresia Membresia { get; set; }
    }
}
