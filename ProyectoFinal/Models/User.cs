using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        // Relación con Status
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string RememberToken { get; set; }

        // Relación uno a muchos con Session
        public ICollection<Session> Sessions { get; set; }
    }
}
