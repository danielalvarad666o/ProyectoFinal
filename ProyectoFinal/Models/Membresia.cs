using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Membresia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int DuracionMeses { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        // Relación uno a muchos con Pago
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
