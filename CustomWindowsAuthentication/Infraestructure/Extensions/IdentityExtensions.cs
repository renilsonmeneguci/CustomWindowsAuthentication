using CustomWindowsAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;

namespace CustomWindowsAuthentication.Infraestructure.Extensions
{
    public static class IdentityExtensions
    {
        private static ApplicationContext db = new ApplicationContext();


        public static bool IsInIdentityRole(this IPrincipal user, string perfil)
        {
            //if (perfil == null) return false;

            var username = user.Identity.Name.Split('\\').LastOrDefault();

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("Asp.NetCookie");

            if (cookie == null)
            {
                var perfisUsuario = db.UsuarioPerfis.Where(p => p.UsuarioLogin.Contains(username)).ToList();

                var cookieText = Encoding.UTF8.GetBytes(string.Join(";", perfisUsuario.Select(p => p.Perfil.Nome)));
                var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookiePermission"));

                HttpCookie cookieObject = new HttpCookie("Asp.NetCookie", encryptedValue);

                //---- Add cookie to cookie collection.
                HttpContext.Current.Response.Cookies.Add(cookieObject);
                cookie = cookieObject;
            }

            string permission = cookie.Value;

            var bytes = Convert.FromBase64String(HttpContext.Current.Request.Cookies["Asp.NetCookie"].Value);
            var output = MachineKey.Unprotect(bytes, "ProtectCookiePermission");
            string result = Encoding.UTF8.GetString(output);

            if (result.Contains("Administradores")) return true;

            var isAuthorized = result.Contains(perfil);

            return true;
        }
    }
}