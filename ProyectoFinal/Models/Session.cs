using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Session
    {
        public string Id { get; set; }

        // Relación con User (opcional)
        public int? UserId { get; set; }
        public User User { get; set; }

        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Payload { get; set; }
        public int LastActivity { get; set; }
    }
}
