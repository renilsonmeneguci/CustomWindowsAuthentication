using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomWindowsAuthentication.Models
{
    public class Perfil
    {
        [Key]
        public Guid PerfilId { get; set; }
        public String Nome { get; set; }
    }
}