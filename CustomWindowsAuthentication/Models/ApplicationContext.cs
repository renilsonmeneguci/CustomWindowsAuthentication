using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomWindowsAuthentication.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("DefaultConnection")
        {

        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}