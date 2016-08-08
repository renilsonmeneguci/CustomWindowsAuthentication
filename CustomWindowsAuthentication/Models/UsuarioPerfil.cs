using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomWindowsAuthentication.Models
{
    public class UsuarioPerfil
    {
        [Key]
        public Guid UsuarioPerfilId { get; set; }
        public String UsuarioLogin { get; set; }
        public Guid PerfilId { get; set; }

        [ForeignKey("PerfilId")]
        public virtual Perfil Perfil { get; set; }
    }
}