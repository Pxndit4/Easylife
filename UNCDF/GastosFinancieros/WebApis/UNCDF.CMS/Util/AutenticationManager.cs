using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS
{
    public class AutenticationManager
    {
        public const string USER_INFO = "CURRRENT_USER";
        public static void SaveLogin(UserIdentity objUser)
        {
            HttpContext.Current.Session.Add(USER_INFO, objUser);
        }

        public static UserIdentity GetUser()
        {
            UserIdentity objUser = HttpContext.Current.Session[USER_INFO] as UserIdentity;
            return objUser;
        }
        public static void SetOpciones(List<MOptions> pOpciones)
        {
            UserIdentity objUser = HttpContext.Current.Session[USER_INFO] as UserIdentity;
            objUser.Opciones = (from c in pOpciones
                                select c.OptionId
                                    ).ToList<int>();
            HttpContext.Current.Session[USER_INFO] = objUser;
        }
        public static void SetPerfil(int idPerfilSelected)
        {
            UserIdentity objUser = HttpContext.Current.Session[USER_INFO] as UserIdentity;
            objUser.IdPerfil = idPerfilSelected;
            HttpContext.Current.Session[USER_INFO] = objUser;
        }

        public static List<int> GetOpciones()
        {
            UserIdentity objUser = HttpContext.Current.Session[USER_INFO] as UserIdentity;
            return objUser.Opciones;
        }
        public static bool IsLoged
        {
            get
            {
                return GetUser() != null;
            }
        }
        public static void Logout()
        {
            if (IsLoged)
            {
                HttpContext.Current.Session.Remove(USER_INFO);
                HttpContext.Current.Session.Abandon();
            }
        }
        public static object EnableDeveloperSession()
        {
            UserIdentity objUser = new UserIdentity();
            objUser.IsAdmin = true;
            objUser.Usuario = "kpalomino";
            objUser.Nombres = "Developer - Session";

            SaveLogin(objUser);
            return null;
        }
    }

    public class UserIdentity
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPerfil { get; set; }
        public string NombreConexion { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public List<int> Opciones { get; set; }
    }
}