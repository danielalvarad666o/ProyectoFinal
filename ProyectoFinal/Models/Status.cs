using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
  
        public class Status
        {
            public int Id { get; set; }
            public string Type { get; set; }

            // Relación uno a muchos con User
            public ICollection<User> Users { get; set; }
        }
    
}
