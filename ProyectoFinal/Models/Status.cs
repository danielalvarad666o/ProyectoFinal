using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        // Relación inversa: Status tiene muchos Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
