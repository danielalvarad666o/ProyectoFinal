using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{

    public class PasswordReset
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
