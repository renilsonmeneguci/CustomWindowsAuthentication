using CustomWindowsAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CustomWindowsAuthentication.Infraestructure.Extensions
{
    public static class IdentityExtensions
    {
        private static ApplicationContext db = new ApplicationContext();


        public static bool IsInIdentityRole(this IPrincipal user, string perfil)
        {
            if (perfil == null) return false;

            var username = user.Identity.Name.Split('\\').LastOrDefault();

            var perfisUsuario = db.UsuarioPerfis.Where(p => p.UsuarioLogin.Contains(username)).ToList();

            if (perfisUsuario.Any(p => p.Perfil.Nome == "Administradores")) return true;

            var isAuthorized = perfisUsuario.Any(p => p.Perfil.Nome == perfil);

            return isAuthorized;
        }
    }
}