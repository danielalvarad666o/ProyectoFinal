using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Membresia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int DuracionMeses { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        // Relación uno a muchos con Pago
        public ICollection<Pago> Pagos { get; set; }
    }
}
