using CustomWindowsAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomWindowsAuthentication.Infraestructure.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private ApplicationContext db = new ApplicationContext();
        private string[] Perfis { get; set; }

        public CustomAuthorize(params string[] roles)
        {
            this.Perfis = roles;
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Perfis == null) return false;

            var username = httpContext.User.Identity.Name;

            var perfisUsuario = db.UsuarioPerfis.Include(p => p.Perfil).Where(p => p.UsuarioLogin == username).ToList();

            var isAuthorized = false;

            foreach(var perfil in perfisUsuario)
            {
                if (Perfis.Contains(perfil.Perfil.Nome))
                {
                    isAuthorized = true;
                }
            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Home",
                                action = "Index"
                            })
                        );
        }
    }
}