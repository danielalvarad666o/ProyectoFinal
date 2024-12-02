using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public DateTime BirthDate { get; set; }

        // Relación con Status
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string RememberToken { get; set; }

        // Relación uno a muchos con Inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}

