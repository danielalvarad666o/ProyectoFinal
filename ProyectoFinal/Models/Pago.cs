using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Membresia")]
        public int MembresiaId { get; set; }
        public Membresia Membresia { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        [MaxLength(20)]
        public string MetodoPago { get; set; } // Ejemplo: Tarjeta, Efectivo
    }
}
